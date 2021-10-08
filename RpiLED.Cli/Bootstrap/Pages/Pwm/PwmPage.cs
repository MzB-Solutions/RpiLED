using EasyConsole;

namespace RpiLED.Cli.Bootstrap.Pages
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