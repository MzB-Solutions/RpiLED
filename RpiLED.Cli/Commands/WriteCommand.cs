using System;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Properties;
using RpiLed.Core;
using RpiLED.Core.Models;

namespace RpiLED.Cli.Commands
{
    public class WriteCommand : ICommand<WriteCommandArgs>
    {
        #region Public Properties

        public WriteCommandArgs Arguments { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Execute()
        {
            var pin = new PinModel(Arguments.Pin, PinService.Gpio);
            Console.WriteLine(Resources.TalkingToPinValue, Arguments.Pin.ToString());
            Console.WriteLine(Resources.WriteValue, Arguments.Value.ToString());
            Console.WriteLine(
                $@"Its direction is ({pin.PinDirection}) and its value is: {pin.PinState}");
            pin.PinWrite(Arguments.Value);
        }

        #endregion Public Methods
    }
}