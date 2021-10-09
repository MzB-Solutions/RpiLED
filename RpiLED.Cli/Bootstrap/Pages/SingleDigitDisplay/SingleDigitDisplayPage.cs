using System;
using EasyConsole;
using RpiLED.Core.Services;

namespace RpiLED.Cli.Bootstrap.Pages.SingleDigitDisplay
{
    public class SingleDigitDisplayPage : Page
    {
        public SingleDigitDisplayPage(EasyConsole.Program program): base("Single Digit Display Output", program)
        {
        }

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
    }
}