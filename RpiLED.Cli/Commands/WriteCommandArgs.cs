using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLED.Cli.Commands
{
    public class WriteCommandArgs
    {
        [Argument("Pin", "p")]
        [HelpText("The number of the physical pin to use. ie: 12")]
        public int Pin { get; set; }

        [Argument("Value", "v")]
        [HelpText("What to write to this pin, false for low and true for high")]
        public bool Value { get; set; }
    }
}
