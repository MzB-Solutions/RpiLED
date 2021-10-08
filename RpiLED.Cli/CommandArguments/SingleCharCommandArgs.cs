using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLed.Cli.CommandArguments
{
    [HelpTextProvider(typeof(SingleCharCommandArgs))]
    public class SingleCharCommandArgs : TypeHelpProvider
    {
        #region Public Properties

        [Argument("character", "c")]
        [HelpText("Provide a single character in the hexadecimal range")]
        public char Character { get; set; }

        #endregion Public Properties

        #region Public Methods

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

        #endregion Public Methods
    }
}