using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Properties;
using RpiLED.Core.Models;
using RpiLED.Core.Services;
using System;
using RpiLed.Core;

namespace RpiLED.Cli.Commands
{
    public class ReadCommand : ICommand<ReadCommandArgs>
    {
        public ReadCommandArgs Arguments { get; set; }

        public void Execute()
        {
            PinModel pin = new(Arguments.Pin, PinService.Gpio);
            Console.WriteLine(Resources.TalkingToPinValue, Arguments.Pin.ToString());
            Console.WriteLine($@"Its direction is ({pin.PinDirection}) and its value is: {pin.PinState}");
        }
    }
}