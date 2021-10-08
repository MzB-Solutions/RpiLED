using System;
using EasyConsole;
using RpiLED.Cli.Bootstrap.Pages;
using RpiLed.Cli.Bootstrap.Pages.Gpio;

namespace RpiLED.Cli.Bootstrap
{
    public class CliMenu : EasyConsole.Program
    {
        public CliMenu() : base("RpiLED.CLI Menu Interface", breadcrumbHeader: true)
        {
            AddPage(new MainPage(this));
            AddPage(new GpioPage(this));
            AddPage(new GpioReadPage(this));
            AddPage(new GpioWritePage(this));
            AddPage(new GpioStatusPage(this));
            AddPage(new PwmPage(this));
            AddPage(new StatusPage(this));
            SetPage<MainPage>();
        }
    }
}