using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EasyConsole;
using RpiLed.Cli.Bootstrap.Pages.Gpio;

namespace RpiLED.Cli.Bootstrap.Pages
{
    public class MainPage : MenuPage
{
    public MainPage(EasyConsole.Program program, ref bool run)
        : base("Main Page", program,
              new Option("GPIO", () => program.NavigateTo<GpioPage>()),
              new Option("PWM", () => program.NavigateTo<PwmPage>()),
              new Option("Pin Status", () => program.NavigateTo<StatusPage>()),
              new Option("Quit", () => RpiLED.Cli.Program.QuitApp(0)))
    {
    }
    

    }
}