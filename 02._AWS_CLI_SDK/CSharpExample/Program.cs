using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

AWSCredentials awsCredentials = FallbackCredentialsFactory.GetCredentials();

AmazonEC2Client client = new(awsCredentials);
DescribeInstancesRequest request = new();

DescribeInstancesResponse response = await client.DescribeInstancesAsync(request);

Console.WriteLine($"Total Reservations: {response.Reservations.Count}");
if (response.Reservations.Count == 0) return;

foreach (Reservation reservation in response.Reservations)
{
    foreach ((Instance? instance, string? name) in from instance in reservation.Instances
         from string name in instance.Tags.Select(tag => tag.Key == "Name" ? tag.Value : "Anonymous")
         select (instance, name))
    {
        Console.WriteLine($"Name: {name}\t\t\tPub. IP: {instance.PublicIpAddress}");
    }
}
