using System;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLED.Cli.Properties;
using RpiLed.Core;
using RpiLED.Core.Models;
using RpiLed.Core.Services;

namespace RpiLED.Cli.Commands
{
    public class CharacterCommand : ICommand<CharacterCommandArgs>
    {
        #region Public Properties

        public CharacterCommandArgs Arguments { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Execute()
        {
            var character = new ShiftRegisterService();
            character.ShiftIn(Arguments.Value);
        }

        #endregion Public Methods
    }
}