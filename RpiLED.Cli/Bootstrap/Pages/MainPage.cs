using EasyConsole;
using RpiLed.Cli.Bootstrap.Pages.Gpio;
using RpiLed.Cli.Bootstrap.Pages.Pwm;
using RpiLED.Cli.Bootstrap.Pages.ShiftRegisterTest;
using RpiLED.Cli.Bootstrap.Pages.SingleDigitDisplay;
using RpiLed.Cli.Bootstrap.Pages.Status;

namespace RpiLED.Cli.Bootstrap.Pages
{
    public class MainPage : MenuPage
    {
        #region Public Constructors

        public MainPage(EasyConsole.Program program)
        : base("Main Page", program,
              new Option("GPIO", () => program.NavigateTo<GpioPage>()),
              new Option("PWM", () => program.NavigateTo<PwmPage>()),
              new Option("Pin Status", () => program.NavigateTo<StatusPage>()),
              new Option("Single Character Display", () => program.NavigateTo<SingleDigitDisplayPage>()),
              new Option("Shift Register Test", () => program.NavigateTo<ShiftRegisterTestPage>()),
              new Option("Quit", () => RpiLED.Cli.Program.QuitApp(0)))
        {
        }

        #endregion Public Constructors
    }
}