using System;
using EasyConsole;
using RpiLed.Core;
using RpiLED.Core.Models;

namespace RpiLed.Cli.Bootstrap.Pages.Gpio
{
    public class GpioStatusPage : Page
    {
        
        public GpioStatusPage(EasyConsole.Program program) : base("GPIO Pin Status", program)
        {
        }

        public override void Display()
        {
            base.Display();
            Output.WriteLine(ConsoleColor.DarkRed,"Not implemented yet!");
            Input.ReadString("Press any key to return");
            base.Program.NavigateHome();
        }
    }
}