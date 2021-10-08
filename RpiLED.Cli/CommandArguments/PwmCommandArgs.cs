using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLed.Cli.CommandArguments
{
    [HelpTextProvider(typeof(PwmCommandArgs))]
    public class PwmCommandArgs : TypeHelpProvider
    {
        #region Public Properties

        [Argument("Pin", "p", Required = true)]
        [HelpText("The number of the physical pin to use. ie: 12")]
        public int Pin { get; set; }

        [Argument("Value", "v", Required = true)]
        [HelpText("What to write to this pin, A value between 0 and 100 percent")]
        public int Value { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void WriteTypeFooter(TypeHelpRequest helpRequest)
        {
            Console.WriteLine();
            Console.WriteLine("►───────────────────────────────────────────────────────────────────────────◄");
        }

        public override void WriteTypeHeader(TypeHelpRequest helpRequest)
        {
            Console.WriteLine(" ╔══════════════════════════╗");
            Console.WriteLine(" ║  'write' ARGUMENT HELP   ║");
            Console.WriteLine(" ╚══════════════════════════╝");
            Console.WriteLine();
        }

        #endregion Public Methods
    }
}