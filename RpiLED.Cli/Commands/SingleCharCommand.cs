using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLed.Cli.CommandArguments;
using RpiLED.Cli.Commands;
using RpiLed.Core.Services;

namespace RpiLed.Cli.Commands
{
    public class SingleCharCommand : ICommand<SingleCharCommandArgs>
    {
        public void Execute()
        {
            var sr = new ShiftRegisterService();
            sr.ShiftIn(Arguments.Character);
        }

        public SingleCharCommandArgs Arguments { get; set; }
    }
}