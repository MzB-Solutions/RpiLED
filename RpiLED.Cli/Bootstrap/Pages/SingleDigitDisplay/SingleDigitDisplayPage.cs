using EasyConsole;
using RpiLED.Core.Services;
using System;

namespace RpiLED.Cli.Bootstrap.Pages.SingleDigitDisplay
{
    public class SingleDigitDisplayPage : Page
    {
        #region Public Constructors

        public SingleDigitDisplayPage(EasyConsole.Program program) : base("Single Digit Display Output", program)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Display()
        {
            base.Display();
            Output.WriteLine(ConsoleColor.Green, "Please input a single character (in hexadecimal range): ");
            var ch = Input.ReadString("Input Character -> ");
            var sr = new ShiftRegisterService();
            sr.ShiftIn(ch[0]);
            Input.ReadString("Press any key to continue");
            Program.NavigateHome();
        }

        #endregion Public Methods
    }
}