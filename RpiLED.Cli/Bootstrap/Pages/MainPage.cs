using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyConsole;

namespace RpiLED.Cli.Bootstrap.Pages
{
    public class MainPage : MenuPage
{
    public MainPage(EasyConsole.Program program)
        : base("Main Page", program,
              new Option("GPIO", () => program.NavigateTo<GpioPage>()),
              new Option("PWM", () => program.NavigateTo<PwmPage>()),
              new Option("Pin Status", () => program.NavigateTo<StatusPage>()),
              new Option("Quit", () => return false;))
    {
    }
}
}