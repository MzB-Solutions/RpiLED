using System;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Properties;

namespace RpiLED.Cli.Commands
{
    public class ReadCommand : ICommand<ReadCommandArgs>
    {

        public ReadCommandArgs Arguments { get; set; }

        public void Execute()
        {
            Console.WriteLine(@"Some awesome output here");
            Console.WriteLine($@"We should be talking to pin ({Arguments.Pin.ToString()}) ..");

        }

    }
}
