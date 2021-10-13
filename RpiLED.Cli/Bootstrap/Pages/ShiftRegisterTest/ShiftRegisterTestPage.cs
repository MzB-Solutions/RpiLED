using System;
using EasyConsole;
using RpiLED.Core.Services;

namespace RpiLED.Cli.Bootstrap.Pages.ShiftRegisterTest
{
    public class ShiftRegisterTestPage : Page
    {
        public ShiftRegisterTestPage(EasyConsole.Program program): base("Shift Register Test Output", program)
        {
        }

        public override void Display()
        {
            base.Display();
            var sr = new ShiftRegisterService();
            for (var i = 0; i < 8; i++)
            {
                sr.SI(ShiftRegisterService.ShiftOutput[i]);
            }
            Input.ReadString("Press any key to continue");
            Program.NavigateHome();
        }
    }
}