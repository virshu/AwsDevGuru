using CommandLine;

namespace WCU
{
    internal class CommandLineOptions
    {
        [Option('p', "profile", Required = false, 
            HelpText = "Profile name to use for credentials. Leave blank to use default or IAM Role credentials")]
        public string? Profile { get;  set; }

        [Option(longName: "wcu", Required = false,
            HelpText = "Write Capacity Units (WCU). Default - 10")]
        public string? Wcu { get; set; }
    }
}
