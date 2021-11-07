using EasyConsole;

namespace RpiLed.Cli.Bootstrap.Pages.Status
{
    public class StatusPage : Page
    {
        #region Public Constructors

        public StatusPage(EasyConsole.Program program) : base("Pin Overview :", program)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Display()
        {
            base.Display();
            Output.WriteLine("This will show an overview of all pins and their states/functions");
            Program.NavigateHome();
        }

        #endregion Public Methods
    }
}