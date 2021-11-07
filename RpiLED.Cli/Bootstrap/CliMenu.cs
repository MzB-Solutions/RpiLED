using RpiLed.Cli.Bootstrap.Pages.Gpio;
using RpiLed.Cli.Bootstrap.Pages.Pwm;
using RpiLed.Cli.Bootstrap.Pages.Status;
using RpiLED.Cli.Bootstrap.Pages;
using RpiLED.Cli.Bootstrap.Pages.ShiftRegisterTest;
using RpiLED.Cli.Bootstrap.Pages.SingleDigitDisplay;

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
            AddPage(new SingleDigitDisplayPage(this));
            AddPage(new ShiftRegisterTestPage(this));
            SetPage<MainPage>();
        }

        #endregion Public Constructors
    }
}