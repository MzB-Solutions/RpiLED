using System;
using EasyConsole;

namespace RpiLED.Cli.Bootstrap
{
    public class CliMenu
    {
        public CliMenu()
        {
            var menu = new EasyConsole.Menu()
                .Add("GPIO", () => Output.WriteLine("GPIO selected..", "green"))
                .Add("PWM", () => Output.WriteLine("PWM selected..", "blue"));
            menu.Display();
        }
    }
}