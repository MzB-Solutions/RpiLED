using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using RpiLed.Core.Services;

namespace RpiLED.Cli.Commands
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