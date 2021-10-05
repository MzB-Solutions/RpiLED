using System;
using EasyConsole;
using RpiLED.Cli.Bootstrap.Pages;

namespace RpiLED.Cli.Bootstrap
{
    public class CliMenu : EasyConsole.Program
    {
        public CliMenu() : base("RpiLED.CLI Menu Interface", breadcrumbHeader: true)
        {
            AddPage(new MainPage(this));
            SetPage<MainPage>();
        }
    }
}