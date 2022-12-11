using CommandLine;

namespace SQS;

internal class CommandLineOptions
{
    [Option('p', "profile", Required = false, 
        HelpText = "Profile name to use for credentials. Leave blank to use default or IAM Role credentials")]
    public string? Profile { get;  set; }

    [Option('r', "read", Required = false, 
        HelpText = "Launch queue reader")]
    public bool Reader { get;  set; }

    [Option('w', "write", Required = false, 
        HelpText = "Launch queue writer")]
    public bool Writer { get;  set; }

}
