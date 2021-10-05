using System.Reflection.Metadata.Ecma335;
using EasyConsole;

namespace RpiLED.Cli.Bootstrap.Pages
{
    internal class PwmPage : MenuPage
    {
        public PwmPage(EasyConsole.Program program) : base("PWM Page", program,
            new Option("NOT IMPLEMENTED YET!!!", () => program.NavigateTo<PwmPage>()))
        {
        }
    }
}