using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Bootstrap;

namespace RpiLED.Cli.Commands
{
    [HelpTextProvider(typeof(ReadCommandArgs))]
    public class ReadCommandArgs : TypeHelpProvider
    {
        public override void WriteTypeHeader(TypeHelpRequest helpRequest)
        {
            Console.WriteLine(" ╔══════════════════════════╗");
            Console.WriteLine(" ║  'read' ARGUMENT HELP    ║");
            Console.WriteLine(" ╚══════════════════════════╝");
            Console.WriteLine();
        }

        public override void WriteTypeFooter(TypeHelpRequest helpRequest)
        {
            Console.WriteLine();
            Console.WriteLine("►───────────────────────────────────────────────────────────────────────────◄");
        }
        [Argument("Pin", "p", Required = true)]
        [HelpText("The number of the physical pin to use. ie: 12")]
        public int Pin { get; set; }

    }
}
