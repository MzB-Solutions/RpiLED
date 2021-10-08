using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLed.Cli.Commands;
using RpiLED.Cli.Commands;
using RpiLED.Cli.Properties;

namespace RpiLED.Cli.Bootstrap
{
    [HelpTextProvider(typeof(AppArguments))]
    public class AppArguments : TypeHelpProvider
    {
        #region Public Properties

        [Command("HELP", "h", "?")]
        [HelpText("Displays the help you are watching at the moment.")]
        [DetailedHelpText(
            "calling this app with a command and then -h display the help of that command. ei: RpiLED.Cli -r -h")]
        public HelpCommand Help { get; set; }

        [Option("KEYWAIT", "k")]
        [HelpText("KeyWait for a keypress after the app terminates.")]
        [DetailedHelpText("Additionally, a line prompting to press a key will be displayed.")]
        public bool KeyWait { get; set; }

        [Command("PWM", "p")]
        [HelpText("This writes a PWM value to the indicated Pin")]
        public PwmCommand Pwm { get; set; }

        [Command("READ", "r")]
        [HelpText("This reads a given pins current state on the board. (see -p)")]
        [DetailedHelpText(
            "Please note: this does NOT (re)set the pin in any way, shape, or form. We simply want to know its value.")]
        public ReadCommand Read { get; set; }

        [Command("WRITE", "w")]
        [HelpText("This writes the value to the given pin. (see -p -v)")]
        public WriteCommand Write { get; set; }

        [Command("SingleCharacter","s")]
        [HelpText("This writes a hexadecimal value via the shiftregister")]
        public SingleCharCommand SingleChar { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void WriteTypeFooter(TypeHelpRequest helpRequest)
        {
            Console.WriteLine();
            Console.WriteLine("►───────────────────────────────────────────────────────────────────────────◄");
            if (!KeyWait) return;
            Console.Write(Resources.ExitPrompt);
            Console.ReadLine();
        }

        public override void WriteTypeHeader(TypeHelpRequest helpRequest)
        {
            Console.WriteLine(" ╔══════════════════════════╗");
            Console.WriteLine(" ║  RpiLED.CLI COMMAND HELP ║");
            Console.WriteLine(" ╚══════════════════════════╝");
            Console.WriteLine();
        }

        #endregion Public Methods
    }
}