using System;
using System.IO;
using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Properties;

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
            if (arguments.Wait)
            {
                Console.Write(Resources.ExitPrompt); Console.ReadLine();
            }
        }
        public override bool HandleException(Exception exception)
        {
            if (exception is IOException)
                Environment.Exit(-1);

            // e.g. do some logging for unknown exceptions...
            Environment.Exit(666);
            return true;
        }
    }
}