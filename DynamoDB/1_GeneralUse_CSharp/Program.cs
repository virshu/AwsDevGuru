using System.Text.Json;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using CommandLine;
using GeneralUse;

// Get credentials
AWSCredentials awsCredentials = FallbackCredentialsFactory.GetCredentials();

bool finish = false;
Parser.Default.ParseArguments<CommandLineOptions>(args)
    .WithParsed(co =>
    {
        if (co.Profile is null) return;
        // required, or govcloud won't work
        AWSConfigs.AWSProfileName = co.Profile;
        SharedCredentialsFile sharedfile = new();
        if (!sharedfile.TryGetProfile(co.Profile, out CredentialProfile profile))
        {
            Console.WriteLine($"Could not find profile {co.Profile}");
            finish = true;
            return;
        }

        AWSCredentialsFactory.TryGetAWSCredentials(profile, sharedfile, out awsCredentials);
    })
    .WithNotParsed(_ => finish = true);
if (finish) return;

AmazonDynamoDBClient client = new(awsCredentials);

Console.WriteLine(">>> Loading cars JSON.");

using StreamReader r = new("cars.json");
string json = r.ReadToEnd();

JsonSerializerOptions options = new()
{
    PropertyNameCaseInsensitive = true
};
List<Car> source = JsonSerializer.Deserialize<List<Car>>(json, options)!;

Console.WriteLine($"Read {source.Count} items");

Console.WriteLine(">>> Creating DynamoDB table");
List<KeySchemaElement> keySchema = new()
{
    new KeySchemaElement("id", KeyType.HASH)
};

List<AttributeDefinition> attributeDefinitions = new()
{
    new()
    {
        AttributeName = "id", AttributeType = ScalarAttributeType.N
    }
};

ProvisionedThroughput provisionedThroughput = new()
{
    ReadCapacityUnits = 5, WriteCapacityUnits = 5
};

try
{
    await client.CreateTableAsync("CarsNE1", keySchema, attributeDefinitions, provisionedThroughput);
}
catch (ResourceInUseException)
{
    Console.WriteLine("Unable to create table. It may already exist");
}

Console.WriteLine("\n>>> Waiting for table state ACTIVE");
DescribeTableResponse describeResponse;
do
{
    Thread.Sleep(1000);
    describeResponse = await client.DescribeTableAsync("CarsNE1");
    Console.WriteLine(describeResponse.Table.TableStatus.Value);
} while (describeResponse.Table.TableStatus.Value is not "ACTIVE");

Console.WriteLine($"\n>>> Loading {source.Count} cars into table");

foreach (PutItemRequest putRequest in source.Select(car => new Dictionary<string, AttributeValue>
     {
         ["id"] = new() {N = car.Id.ToString()},
         ["year"]= new() {N = car.Year.ToString()},
         ["make"]= new() {S = car.Make},
         ["model"]= new() {S = car.Model}
     }).Select(item => new PutItemRequest
         {
         TableName = "CarsNE1",
         Item = item
     }))
{
    await client.PutItemAsync(putRequest);
}

