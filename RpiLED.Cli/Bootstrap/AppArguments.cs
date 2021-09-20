using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Commands;

namespace RpiLED.Cli.Bootstrap
{
    public class AppArguments
    {
        [Command("read", "r", IsDefaultCommand = true)]
        [HelpText("This reads a given pins current state on the board. (see -p)")]
        public ReadCommand Read { get; set; }

    }
}