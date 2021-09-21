using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLED.Cli.Commands
{
    public class ReadCommandArgs
    {
        [Argument("Pin", "p")]
        [HelpText("The number of the physical pin to use. ie: 12")]
        public int Pin { get; set; } = 12;

    }
}
