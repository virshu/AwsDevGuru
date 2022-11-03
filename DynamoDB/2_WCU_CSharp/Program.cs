using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using CommandLine;
using System.Text.Json;
using WCU;

// Get credentials
AWSCredentials awsCredentials = FallbackCredentialsFactory.GetCredentials();

bool finish = false;
int wcu = 10;
Parser.Default.ParseArguments<CommandLineOptions>(args)
    .WithParsed(co =>
    {
        if (co.Wcu is not null)
        {
            wcu = int.Parse(co.Wcu);
        }
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
const string tableName = "CarsNE2";

Console.WriteLine(">>> Loading cars JSON.");

using StreamReader r = new("cars.json");
string json = r.ReadToEnd();

JsonSerializerOptions options = new()
{
    PropertyNameCaseInsensitive = true
};
List<Car> source = JsonSerializer.Deserialize<List<Car>>(json, options)!;
int count = source.Count;
Console.WriteLine($"Read {count} items");

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
    ReadCapacityUnits = 5, 
    WriteCapacityUnits = wcu
};

try
{
    await client.CreateTableAsync(tableName, keySchema, attributeDefinitions, provisionedThroughput);
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
    describeResponse = await client.DescribeTableAsync(tableName);
    Console.WriteLine(describeResponse.Table.TableStatus.Value);
} while (describeResponse.Table.TableStatus.Value is not "ACTIVE");

Console.WriteLine($"\n>>> Loading {count} cars into table");

foreach (PutItemRequest putRequest in source.Select(car => new Dictionary<string, AttributeValue>
         {
             ["id"] = new() {N = car.Id.ToString()},
             ["year"]= new() {N = car.Year.ToString()},
             ["make"]= new() {S = car.Make},
             ["model"]= new() {S = car.Model}
         }).Select(item => new PutItemRequest
         {
             TableName = tableName,
             Item = item
         }))
{
    await client.PutItemAsync(putRequest);
    count--;
    if (count % 200 == 0)
    {
        Console.WriteLine($"{count} items remaining.");
    }
    if (count == 16599 && wcu == 10) {
        Console.WriteLine("\nInserts are probably going pretty slow right now.  Kill the app, delete the table (./deleteTable.sh), change 'var wcu' to 100, and run it again.");
        Console.WriteLine("Observe the change in insert speed.\n");  
    }

}