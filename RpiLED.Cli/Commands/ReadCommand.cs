using System;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Properties;
using RpiLED.Core.Models;
using RpiLED.Core.Services;
using PinMode = System.Device.Gpio.PinMode;

namespace RpiLED.Cli.Commands
{
    public class ReadCommand : ICommand<ReadCommandArgs>
    {

        public ReadCommandArgs Arguments { get; set; }

        public void Execute()
        {
            PinModel pin = new PinModel(PinScheme.Physical, Arguments.Pin);
            Console.WriteLine($@"We should be talking to pin ({Arguments.Pin.ToString()}) ..");
            Console.WriteLine($@"Its direction is ({pin.PinDirection.ToString()}) and its value is: {pin.PinState.ToString()}");

        }

    }
}
