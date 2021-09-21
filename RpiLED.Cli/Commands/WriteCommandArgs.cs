using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLED.Cli.Commands
{
    [HelpTextProvider(typeof(WriteCommandArgs))]
    public class WriteCommandArgs : TypeHelpProvider
    {
        public override void WriteTypeHeader(TypeHelpRequest helpRequest)
        {
            Console.WriteLine(" ╔══════════════════════════╗");
            Console.WriteLine(" ║  'write' ARGUMENT HELP   ║");
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

        [Argument("Value", "v", Required = false)]
        [HelpText("What to write to this pin, false for low and true for high")]
        [DetailedHelpText("If no value is given we apply a low signal to the pin (ie: false)")]
        public bool Value { get; set; } = false;
    }
}
