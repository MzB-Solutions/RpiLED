using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace RpiLED.Cli.Bootstrap
{
    public class AppBootstrap : ConsoleApplication<AppArguments>
    {
        public AppBootstrap(ICommandLineEngine commandLineEngine) : base(commandLineEngine)
        {
            // some pre-init tasks if necessary
        }
        public override void RunWith(AppArguments arguments)
        {
            Console.WriteLine("We should not really get output here..");
        }
    }
}