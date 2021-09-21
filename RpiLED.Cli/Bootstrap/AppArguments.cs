using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Commands;

namespace RpiLED.Cli.Bootstrap
{
   [HelpTextProvider(typeof(AppArguments))]
    public class AppArguments : TypeHelpProvider
    {
        public override void WriteTypeHeader(TypeHelpRequest helpRequest)
        {
            Console.WriteLine(" ╔══════════════════════════╗");
            Console.WriteLine(" ║  XCOPY ARGUMENT HELP     ║");
            Console.WriteLine(" ╚══════════════════════════╝");
            Console.WriteLine();
        }

        public override void WriteTypeFooter(TypeHelpRequest helpRequest)
        {
            Console.WriteLine();
            Console.WriteLine("►───────────────────────────────────────────────────────────────────────────◄");
        }
        
        [Command("Read", "r")]
        [HelpText("This reads a given pins current state on the board. (see -p)")]
        public ReadCommand Read { get; set; }

        [Command("Write", "w")]
        [HelpText("This writes the value to the given pin. (see -p -v)")]
        public WriteCommand Write { get; set; }

        [Command("Help", "h")]
        [HelpText("Displays the help you are watching at the moment.")]
        [DetailedHelpText("calling this app with a command and then -h display the help of that command. ei: RpiLED.Cli -r -h")]
        public HelpCommand Help { get; set; }

        [Option("KeypressWait", "k")]
        [HelpText("Wait for a keypress after the app terminates.")]
        public bool Wait { get; set; }

    }
}