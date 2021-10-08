using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLed.Cli.CommandArguments
{
    [HelpTextProvider(typeof(ReadCommandArgs))]
    public class ReadCommandArgs : TypeHelpProvider
    {
        [Argument("Pin", "p", Required = true)]
        [HelpText("The number of the physical pin to use. ie: 12")]
        public int Pin { get; set; }

        public override void WriteTypeFooter(TypeHelpRequest helpRequest)
        {
            Console.WriteLine();
            Console.WriteLine("►───────────────────────────────────────────────────────────────────────────◄");
        }

        public override void WriteTypeHeader(TypeHelpRequest helpRequest)
        {
            Console.WriteLine(" ╔══════════════════════════╗");
            Console.WriteLine(" ║  'read' ARGUMENT HELP    ║");
            Console.WriteLine(" ╚══════════════════════════╝");
            Console.WriteLine();
        }
    }
}