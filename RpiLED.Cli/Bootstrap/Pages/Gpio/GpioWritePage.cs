using EasyConsole;
using RpiLed.Core;
using RpiLED.Core.Models;
using System;

namespace RpiLed.Cli.Bootstrap.Pages.Gpio
{
    public class GpioWritePage : Page
    {
        #region Private Fields

        private int _pinId;

        #endregion Private Fields

        #region Public Constructors

        public GpioWritePage(Program program) : base("Write a value to GPIO a pin:", program)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Display()
        {
            base.Display();
            Output.WriteLine(ConsoleColor.Blue, "Select Pin :");
            _pinId = Input.ReadInt("Please provide Pin number (1-40):", 1, 40);
            var pin = new PinModel(_pinId, PinService.Gpio);
            Output.WriteLine(ConsoleColor.Blue, $"Pin value: {pin.PinState}");
            var input = Input.ReadInt($"Please provde an INT value indicating Off(0) or On(1):", 0, 1);
            switch (input)
            {
                case 0:
                    pin.PinWrite(false);
                    break;

                case 1:
                    pin.PinWrite(true);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Please only provide 0 (zero/null) or 1(one) values");
            }
            Output.WriteLine(ConsoleColor.Blue, $"Pin value: {pin.PinState}");
            Input.ReadString("Press any key to return");
            Program.NavigateHome();
        }

        #endregion Public Methods
    }
}