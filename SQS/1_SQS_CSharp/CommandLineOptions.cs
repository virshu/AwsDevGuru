using CommandLine;

namespace SQS;

internal class CommandLineOptions
{
    [Option('p', "profile", Required = false, 
        HelpText = "Profile name to use for credentials. Leave blank to use default or IAM Role credentials")]
    public string? Profile { get;  set; }

}
