using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Properties;
using System;
using System.IO;

namespace RpiLED.Cli.Bootstrap
{
    public class AppBootstrap : ConsoleApplication<AppArguments>
    {
        #region Public Constructors

        public AppBootstrap(ICommandLineEngine commandLineEngine) : base(commandLineEngine)
        {
            // some pre-init tasks if necessary
            //if (!HasArguments)
            //{
            //    commandLineEngine.PrintHelp<AppArguments>(null);
            //    throw new MissingCommandLineArgumentException("AppArguments");
            //}
        }

        #endregion Public Constructors

        #region Public Methods

        public override bool HandleException(Exception exception)
        {
            if (exception is IOException)
            {
                Console.WriteLine(Resources.IOExceptionMsg, exception.Message);
                Environment.Exit(1);
            }

            if (exception is MissingCommandLineArgumentException) Environment.Exit(-2);
            // e.g. do some logging for unknown exceptions...
            Environment.Exit(666);
            return true;
        }

        public override void RunWith(AppArguments arguments)
        {
            if (arguments.KeyWait)
            {
                Console.Write(Resources.ExitPrompt);
                Console.ReadLine();
            }
        }

        #endregion Public Methods
    }
}