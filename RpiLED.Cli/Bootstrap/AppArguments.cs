using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Commands;
using RpiLED.Cli.Properties;

namespace RpiLED.Cli.Bootstrap
{
    [HelpTextProvider(typeof(AppArguments))]
    public class AppArguments : TypeHelpProvider
    {
        public override void WriteTypeHeader(TypeHelpRequest helpRequest)
        {
            Console.WriteLine(" ╔══════════════════════════╗");
            Console.WriteLine(" ║  RpiLED.CLI COMMAND HELP ║");
            Console.WriteLine(" ╚══════════════════════════╝");
            Console.WriteLine();
        }

        public override void WriteTypeFooter(TypeHelpRequest helpRequest)
        {
            Console.WriteLine();
            Console.WriteLine("►───────────────────────────────────────────────────────────────────────────◄");
            if (this.KeyWait)
            {
                Console.Write(Resources.ExitPrompt); Console.ReadLine();
            }
        }

        [Command("READ", "r")]
        [HelpText("This reads a given pins current state on the board. (see -p)")]
        [DetailedHelpText("Please note: this does NOT (re)set the pin in any way, shape, or form. We simply want to know its value.")]
        public ReadCommand Read { get; set; }

        [Command("WRITE", "w")]
        [HelpText("This writes the value to the given pin. (see -p -v)")]
        public WriteCommand Write { get; set; }

        [Command("HELP", new[] { "h", "?" })]
        [HelpText("Displays the help you are watching at the moment.")]
        [DetailedHelpText("calling this app with a command and then -h display the help of that command. ei: RpiLED.Cli -r -h")]
        public HelpCommand Help { get; set; }

        [Option("KEYWAIT", "k")]
        [HelpText("KeyWait for a keypress after the app terminates.")]
        [DetailedHelpText("Additionally, a line prompting to press a key will be displayed.")]
        public bool KeyWait { get; set; }

    }
}