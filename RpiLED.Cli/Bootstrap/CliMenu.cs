using System;
using EasyConsole;
using RpiLED.Cli.Bootstrap.Pages;
using RpiLed.Cli.Bootstrap.Pages.Gpio;

namespace RpiLED.Cli.Bootstrap
{
    public class CliMenu : EasyConsole.Program
    {
        private bool _isRunning = false;

        public bool IsRunning
        {
            get => _isRunning;
            set => _isRunning = value;
        }

        public CliMenu() : base("RpiLED.CLI Menu Interface", breadcrumbHeader: true)
        {
            IsRunning = true;
            AddPage(new MainPage(this, ref _isRunning));
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