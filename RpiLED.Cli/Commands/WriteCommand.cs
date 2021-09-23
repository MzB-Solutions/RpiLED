using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Properties;
using RpiLED.Core.Models;
using RpiLED.Core.Services;
using System;

namespace RpiLED.Cli.Commands
{
    public class WriteCommand : ICommand<WriteCommandArgs>
    {
        public WriteCommandArgs Arguments { get; set; }

        public void Execute()
        {
            PinModel pin = new PinModel(PinScheme.Physical, Arguments.Pin);
            Console.WriteLine(Resources.TalkingToPinValue, Arguments.Pin.ToString());
            Console.WriteLine(Resources.WriteValue, Arguments.Value.ToString());
            Console.WriteLine($@"Its direction is ({pin.PinDirection.ToString()}) and its value is: {pin.PinState.ToString()}");
            pin.PinWrite(Arguments.Value);
        }
    }
}