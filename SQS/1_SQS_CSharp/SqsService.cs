using System.Text.Json;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;
using CommandLine;
using Microsoft.Extensions.Configuration;

namespace SQS;

public interface ISqsService
{
    Task Run(IEnumerable<string> args);
}

public class SqsService : ISqsService
{
    private readonly IConfiguration _config;
    private AmazonSQSClient? _sqs;
    private string? _queueUrl;

    public SqsService(IConfiguration config)
    {
        _config = config;
    }

    public async Task Run(IEnumerable<string> args)
    {
        string? profile = null;
        bool read = false;
        bool write = false;
        bool finish = false;
        Parser.Default.ParseArguments<CommandLineOptions>(args)
            .WithParsed(co =>
            {
                profile = co.Profile;
                read = co.Reader;
                write = co.Writer;
            })
            .WithNotParsed(_ => finish = true);
        if (finish) return;

        if (read && write)
        {
            Console.WriteLine("Do you want Write or Read messages? You can't do both!");
            return;
        }
        GetAwsCredentials(profile);
        if (_sqs is null)
        {
            Console.WriteLine("Error: Couldn't Authenticate");
            return;
        }

        if (read || write)
        {
            GetQueueUrlRequest request = new()
            {
                QueueName = _config["queueName"]
            };
            GetQueueUrlResponse? response =await _sqs.GetQueueUrlAsync(request);
            if (response is null)
            {
                Console.WriteLine($"Error: Couldn't find queue {_config["queueName"]}");
                return;
            }
            _queueUrl = response.QueueUrl;
        }
        if (read)
        {
            await ConsumeMessagesAsync();
            return;
        }

        if (write)
        {
            await ProduceMessagesAsync();
            return;
        }

        await CreateQueueAsync();
    }

    private async Task ConsumeMessagesAsync()
    {
        Console.WriteLine("> Polling for a new message");
        if (_sqs is null)
        {
            throw new NullReferenceException("Posting to empty Queue");
        }

        ReceiveMessageRequest request = new()
        {
            QueueUrl = _queueUrl
        };
        ReceiveMessageResponse? response = await _sqs.ReceiveMessageAsync(request);
        if (response.Messages.Count > 0)
        {
            foreach (Message message in response.Messages)
            {
                Console.WriteLine(message.Body);
            }
        }
    }

    private async Task ProduceMessagesAsync()
    {
        Random rnd = new();
        int messageTotal = _config.GetValue<int>("messageTotal");
        string[] contestants = _config.GetSection("contestants").Get<string[]>()!;
        Console.WriteLine($"> Casting {messageTotal} votes to url: {_queueUrl}");
        Console.WriteLine("Note: On send, only errors are logged to the console.  Silence == success");

        if (_sqs is null)
        {
            throw new NullReferenceException("Posting to empty Queue");
        }

        for (int i = 0; i < messageTotal; i++)
        {
            string c = contestants[rnd.Next(contestants.Length)];
            string vote = $"voteId: {i}, voteFor: {c}";

            SendMessageRequest request = new()
            {
                DelaySeconds = 0,
                MessageAttributes = new()
                {
                    ["epochms"] = new()
                    {
                        DataType = "Number",
                        StringValue = DateTime.Now.Ticks.ToString()
                    }
                },
                MessageBody = vote,
                QueueUrl = _queueUrl
            };
            await _sqs.SendMessageAsync(request);
        }
    }

    private async Task CreateQueueAsync()
    {
        string QueueName = _config["queueName"]!;
        CreateQueueResponse result;
        try
        {
            result = await _sqs!.CreateQueueAsync(QueueName);
        }
        catch (AmazonSQSException e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return;
        }
        Console.WriteLine($"Sucess: {JsonSerializer.Serialize(result)}");
    }

    private void GetAwsCredentials(string? profileName)
    {
        AWSCredentials awsCredentials = FallbackCredentialsFactory.GetCredentials();
        if (profileName is not null)
        {
            // required, or govcloud won't work
            AWSConfigs.AWSProfileName = profileName;
            SharedCredentialsFile sharedfile = new();
            if (!sharedfile.TryGetProfile(profileName, out CredentialProfile profile))
            {
                Console.WriteLine($"Could not find profile {profileName}");
                return;
            }

            AWSCredentialsFactory.TryGetAWSCredentials(profile, sharedfile, out awsCredentials);
        }

        _sqs = new(awsCredentials);
    }
}