using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLED.Cli.Commands
{
    public class ReadCommandArgs
    {

        [Argument("Pin", "p")]
        public int Pin { get; set; }

        [Option("WaitForKey", "w")]
        public bool Wait { get; set; }
    }
}
