using System;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Properties;
using RpiLed.Core;
using RpiLED.Core.Models;

namespace RpiLED.Cli.Commands
{
    public class PwmCommand : ICommand<PwmCommandArgs>
    {
        #region Public Properties

        public PwmCommandArgs Arguments { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Execute()
        {
            var pin = new PinModel(PinScheme.Physical, Arguments.Pin);
            Console.WriteLine(Resources.TalkingToPinValue, Arguments.Pin.ToString());
            Console.WriteLine(Resources.WriteValue, Arguments.Value.ToString());
            Console.WriteLine(
                $@"Its direction is ({pin.PinDirection}) and its value is: {pin.PinState}");
            pin.PinWrite(Arguments.Value);
        }

        #endregion Public Methods
    }
}