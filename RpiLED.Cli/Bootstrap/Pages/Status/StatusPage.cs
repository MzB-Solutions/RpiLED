using EasyConsole;

namespace RpiLED.Cli.Bootstrap.Pages
{
    public class StatusPage : Page
    {
        public StatusPage(EasyConsole.Program program) : base("Pin Overview :", program)
        {
                
        }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("This will show an overview of all pins and their states/functions");
            Program.NavigateHome();
        }
    }
}