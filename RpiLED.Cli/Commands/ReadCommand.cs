using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLed.Cli.CommandArguments;
using RpiLed.Core;
using RpiLED.Cli.Properties;
using RpiLED.Core.Models;
using System;

namespace RpiLED.Cli.Commands
{
    public class ReadCommand : ICommand<ReadCommandArgs>
    {
        #region Public Properties

        public ReadCommandArgs Arguments { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Execute()
        {
            PinModel pin = new(Arguments.Pin, PinService.Gpio);
            Console.WriteLine(Resources.TalkingToPinValue, Arguments.Pin.ToString());
            Console.WriteLine($@"Its direction is ({pin.PinDirection}) and its value is: {pin.PinState}");
        }

        #endregion Public Methods
    }
}