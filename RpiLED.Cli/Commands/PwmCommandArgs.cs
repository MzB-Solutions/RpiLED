using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLED.Cli.Commands
{
    [HelpTextProvider(typeof(PwmCommandArgs))]
    public class PwmCommandArgs : TypeHelpProvider
    {
        #region Public Properties

        [Argument("Pin", "p", Required = true)]
        [HelpText("The number of the physical pin to use. ie: 12")]
        public int Pin { get; set; }

        [Argument("Value", "v", Required = false)]
        [HelpText("What to write to this pin, A value between 0 and 100 percent")]
        [DetailedHelpText("If no value is given we apply a low signal to the pin (ie: 0)")]
        public int Value { get; set; } = 0;

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