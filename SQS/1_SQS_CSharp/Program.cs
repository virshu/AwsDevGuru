using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using CommandLine;
using SQS;
using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;

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

using StreamReader r = new("settings.json");
string json = r.ReadToEnd();

JsonElement source = JsonSerializer.Deserialize<JsonElement>(json)!;

AmazonSQSClient sqs = new(awsCredentials);
CreateQueueRequest request = new()
{
    QueueName = source.GetProperty("queueName").GetString()
};
string? QueueName = source.GetProperty("queueName").GetString();
CreateQueueResponse? result = await sqs.CreateQueueAsync(QueueName);
Console.WriteLine(result is null ? "Error" : $"Sucess: {result.QueueUrl}");

