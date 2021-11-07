using EasyConsole;

namespace RpiLed.Cli.Bootstrap.Pages.Pwm
{
    internal class PwmPage : MenuPage
    {
        #region Public Constructors

        public PwmPage(EasyConsole.Program program) : base("PWM Page", program,
            new Option("NOT IMPLEMENTED YET!!!", () => program.NavigateTo<PwmPage>()))
        {
        }

        #endregion Public Constructors
    }
}