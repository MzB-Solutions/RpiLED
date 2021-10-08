using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLed.Cli.CommandArguments;
using RpiLed.Core.Services;

namespace RpiLED.Cli.Commands
{
    public class SingleCharCommand : ICommand<SingleCharCommandArgs>
    {
        #region Public Properties

        public SingleCharCommandArgs Arguments { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Execute()
        {
            var sr = new ShiftRegisterService();
            sr.ShiftIn(Arguments.Character);
        }

        #endregion Public Methods
    }
}