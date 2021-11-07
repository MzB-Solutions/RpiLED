using EasyConsole;
using System;

namespace RpiLed.Cli.Bootstrap.Pages.Gpio
{
    public class GpioStatusPage : Page
    {
        #region Public Constructors

        public GpioStatusPage(EasyConsole.Program program) : base("GPIO Pin Status", program)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Display()
        {
            base.Display();
            Output.WriteLine(ConsoleColor.DarkRed, "Not implemented yet!");
            Input.ReadString("Press any key to return");
            Program.NavigateHome();
        }

        #endregion Public Methods
    }
}