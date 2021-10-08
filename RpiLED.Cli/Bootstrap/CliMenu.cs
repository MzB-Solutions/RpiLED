using RpiLed.Cli.Bootstrap.Pages.Gpio;
using RpiLED.Cli.Bootstrap.Pages;

namespace RpiLED.Cli.Bootstrap
{
    public class CliMenu : EasyConsole.Program
    {
        #region Public Constructors

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

        #endregion Public Constructors
    }
}