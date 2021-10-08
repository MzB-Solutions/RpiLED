using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLED.Cli.Commands
{
    [HelpTextProvider(typeof(CharacterCommandArgs))]
    public class CharacterCommandArgs : TypeHelpProvider
    {
        [Argument("Value", "v", Required = true)]
        [HelpText("What to write to this pin, [any valid single-digit hexadecimal value]")]
        public char Value { get; set; };

        public override void WriteTypeFooter(TypeHelpRequest helpRequest)
        {
            Console.WriteLine();
            Console.WriteLine("►───────────────────────────────────────────────────────────────────────────◄");
        }

        public override void WriteTypeHeader(TypeHelpRequest helpRequest)
        {
            Console.WriteLine(" ╔══════════════════════════════╗");
            Console.WriteLine(" ║  'character' ARGUMENT HELP   ║");
            Console.WriteLine(" ╚══════════════════════════════╝");
            Console.WriteLine();
        }
    }
}