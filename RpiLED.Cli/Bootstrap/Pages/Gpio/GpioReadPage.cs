using System;
using EasyConsole;
using RpiLed.Core;
using RpiLED.Core.Models;

namespace RpiLed.Cli.Bootstrap.Pages.Gpio
{
    public class GpioReadPage : Page
    {
        private int _pinId;
        public GpioReadPage(Program program) : base("Read GPIO Pin", program)
        {
        }

        public override void Display()
        {
            base.Display();
            Output.WriteLine(ConsoleColor.Blue,"Select Pin :");
            _pinId = Input.ReadInt("Please provide Pin number", 1, 40);
            var pin = new PinModel(_pinId, PinService.Gpio);
            Output.WriteLine(ConsoleColor.Blue, $"Pin value: {pin.PinState}");
        }
    }
}