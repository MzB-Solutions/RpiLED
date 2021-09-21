using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Commands;

namespace RpiLED.Cli.Bootstrap
{
    public class AppArguments
    {
        [Command("Read", "r")]
        [HelpText("This reads a given pins current state on the board. (see -p)")]
        public ReadCommand Read { get; set; }

        [Command("Write", "w")]
        [HelpText("This writes the value to the given pin. (see -p -v)")]
        public WriteCommand Write { get; set; }

        [Option("PauseAfterTerminate", "p")]
        [HelpText("Wait for a keypress after the app terminates.")]
        public bool Wait { get; set; }

        [Command("Help", "h", "?")]
        [HelpText("Displays the help you are watching at the moment.")]
        public HelpCommand Help { get; set; }
    }
}