using System;
using ConsoLovers.ConsoleToolkit.Core;
using RpiLED.Cli.Bootstrap;

namespace RpiLED.Cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            ConsoleApplicationManager.For<AppBootstrap>()
                .SetWindowHeight(80)
                .SetWindowWidth(300)
                .SetWindowTitle("Awesome Title")
                .Run(args);
        }
    }
}
