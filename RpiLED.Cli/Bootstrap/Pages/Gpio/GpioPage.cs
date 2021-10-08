using EasyConsole;

namespace RpiLed.Cli.Bootstrap.Pages.Gpio
{
    public class GpioPage : MenuPage
    {
        #region Public Constructors

        public GpioPage(EasyConsole.Program program) : base("GPIO Page", program,
            new Option("Read", () => program.NavigateTo<GpioReadPage>()),
            new Option("Write", () => program.NavigateTo<GpioWritePage>()),
            new Option("Pin Status", () => program.NavigateTo<GpioStatusPage>()))
        {
        }

        #endregion Public Constructors
    }
}