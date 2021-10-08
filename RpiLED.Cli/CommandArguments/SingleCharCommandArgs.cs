using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLed.Cli.CommandArguments
{
    [HelpTextProvider(typeof(SingleCharCommandArgs))]
    public class SingleCharCommandArgs : TypeHelpProvider
    {
        [Argument("character","c")]
        [HelpText("Provide a single character in the hexadecimal range")]
        public char Character { get; set; }

        public override void WriteTypeFooter(TypeHelpRequest helpRequest)
        {
            Console.WriteLine();
            Console.WriteLine("►───────────────────────────────────────────────────────────────────────────◄");
        }

        public override void WriteTypeHeader(TypeHelpRequest helpRequest)
        {
            Console.WriteLine(" ╔═══════════════════════════╗");
            Console.WriteLine(" ║  'single' ARGUMENT HELP   ║");
            Console.WriteLine(" ╚═══════════════════════════╝");
            Console.WriteLine();
        }
    }
}