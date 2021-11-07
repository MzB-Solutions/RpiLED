using EasyConsole;
using RpiLed.Core;
using RpiLED.Core.Models;
using System;

namespace RpiLed.Cli.Bootstrap.Pages.Gpio
{
    public class GpioReadPage : Page
    {
        #region Private Fields

        private int _pinId;

        #endregion Private Fields

        #region Public Constructors

        public GpioReadPage(Program program) : base("Read GPIO Pin", program)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Display()
        {
            base.Display();
            Output.WriteLine(ConsoleColor.Blue, "Select Pin :");
            _pinId = Input.ReadInt("Please provide Pin number", 1, 40);
            var pin = new PinModel(_pinId, PinService.Gpio);
            Output.WriteLine(ConsoleColor.Blue, $"Pin value: {pin.PinState}");
            Input.ReadString("Press any key to return");
            Program.NavigateHome();
        }

        #endregion Public Methods
    }
}