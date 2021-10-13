using EasyConsole;
using RpiLED.Core.Services;
using System;

namespace RpiLED.Cli.Bootstrap.Pages.ShiftRegisterTest
{
    public class ShiftRegisterTestPage : Page
    {
        #region Public Constructors

        public ShiftRegisterTestPage(EasyConsole.Program program) : base("Shift Register Test Output", program)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Display()
        {
            base.Display();
            var sr = new ShiftRegisterService();
            Output.WriteLine(ConsoleColor.Green, "Running ShiftRegister tests");
            sr.RunTest();
            Output.WriteLine(ConsoleColor.Blue, "Done");
            Input.ReadString("Press any key to continue");
            Program.NavigateHome();
        }

        #endregion Public Methods
    }
}