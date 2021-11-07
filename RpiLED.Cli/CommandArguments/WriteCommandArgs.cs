using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLed.Cli.CommandArguments
{
    [HelpTextProvider(typeof(WriteCommandArgs))]
    public class WriteCommandArgs : TypeHelpProvider
    {
        #region Public Properties

        [Argument("Pin", "p", Required = true)]
        [HelpText("The number of the physical pin to use. ie: 12")]
        public int Pin { get; set; }

        [Argument("Value", "v", Required = false)]
        [HelpText("What to write to this pin, Default: false [false = low]  [true = high]")]
        [DetailedHelpText("If no value is given we apply a low signal to the pin (ie: false)")]
        public bool Value { get; set; } = false;

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