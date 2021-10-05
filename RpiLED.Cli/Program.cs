using System;
using System.Linq;
using ConsoLovers.ConsoleToolkit.Core;
using RpiLED.Cli.Bootstrap;
using EasyConsole;

namespace RpiLED.Cli
{
    internal class Program
    {
        /// <summary>
        /// Do a clean exit with an errorcode
        /// </summary>
        /// <param name="exitCode">pass in any reasonable int as return or error code, 0 for success</param>
        public static void QuitApp(int exitCode = 0)
        {
            System.Environment.Exit(exitCode);
        }

        /// <summary>
        /// The main entry point for RpiLED.Cli
        /// </summary>
        /// <param name="args">command line parameters passed in from the environment</param>
        /// TODO(smzb): We should be able to read a config file and define wether interactive mode is enabled or not.
        private static void Main(string[] args)
        {
            Console.WriteLine($@"Number of Arguments: {args.Length}");
            if (args.Length == 0)
            {
                // Spawn a new cli-menu
                new CliMenu().Run();
            }
            //Console.WriteLine("Hello World!");
            ConsoleApplicationManager.For<AppBootstrap>()
                .SetWindowHeight(80)
                .SetWindowWidth(300)
                .SetWindowTitle("Awesome Title")
                .Run(args);
        }
    }
}