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
              new Option("Page 2", () => program.NavigateTo<PwmPage>()),
              new Option("Input", () => program.NavigateTo<StatusPage>()),
              new Option("Quit", () => return false;))
    {
    }
}
}