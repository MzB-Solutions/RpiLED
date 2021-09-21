using System;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Properties;

namespace RpiLED.Cli.Commands
{
    public class WriteCommand : ICommand<WriteCommandArgs>
    {

        public WriteCommandArgs Arguments { get; set; }

        public void Execute()
        {
            Console.WriteLine(Resources.WelcomeString);
            Console.WriteLine($@"Writing [{Arguments.Value.ToString()}] to physical pin ({Arguments.Pin.ToString()}) !");
        }

    }
}
