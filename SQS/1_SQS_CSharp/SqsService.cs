using System.Text.Json;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;
using CommandLine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VotesData;

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
    private readonly VotesContext _db;

    public SqsService(IConfiguration config, VotesContext db)
    {
        _config = config;
        _db = db;
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
                QueueName = _config["SQS:queueName"]
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
        while (response.Messages.Count > 0)
        {
            foreach (Message message in response.Messages)
            {
                TvVote vote = JsonSerializer.Deserialize<TvVote>(message.Body)!;
                Votes? voteName = await _db.Votes.SingleOrDefaultAsync(v => v.Name == vote.VoteFor);
                if (voteName != null)
                {
                    voteName.VoteCount++;
                }
                else
                {
                    voteName = new Votes
                    {
                        VoteCount = 1,
                        Name = vote.VoteFor
                    };
                    await _db.Votes.AddAsync(voteName);
                }

                await _db.SaveChangesAsync();
                await _sqs.DeleteMessageAsync(_queueUrl, message.ReceiptHandle);
            }

            response = await _sqs.ReceiveMessageAsync(request);
        }
    }

    private async Task ProduceMessagesAsync()
    {
        if (_sqs is null)
        {
            throw new NullReferenceException("Posting to empty Queue");
        }

        Random rnd = new();
        int messageTotal = _config.GetValue<int>("SQS:messageTotal");
        string[] contestants = _config.GetSection("contestants").Get<string[]>()!;
        Console.WriteLine($"> Casting {messageTotal} votes to url: {_queueUrl}");
        Console.WriteLine("Note: On send, only errors are logged to the console.  Silence == success");

        for (int i = 0; i < messageTotal; i++)
        {
            string contestant = contestants[rnd.Next(contestants.Length)];
            TvVote vote = new()
            {
                VoteId = i,
                VoteFor = contestant
            };

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
                MessageBody = JsonSerializer.Serialize(vote),
                QueueUrl = _queueUrl
            };
            await _sqs.SendMessageAsync(request);
        }
    }

    private async Task CreateQueueAsync()
    {
        string queueName = _config["Sqs:queueName"]!;
        CreateQueueResponse result;
        try
        {
            result = await _sqs!.CreateQueueAsync(queueName);
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

internal record TvVote
{
    public int VoteId { get; init; }
    public string VoteFor { get; init; } = null!;
}
