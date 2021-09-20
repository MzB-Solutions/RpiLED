using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RpiLED.Core.Services;
using PinMode = System.Device.Gpio.PinMode;

namespace RpiLED.Core.Models
{
    internal class PinModel
    {
        internal GpioService ioService;

        private static int pinLocation;

        public PinValue PinState { get; private set; }
        public PinMode PinDirection { get; private set; }

        public PinModel(PinScheme scheme, int pinNumber)
        {
            ioService = new GpioService();
            pinLocation = pinNumber;
            _getPinValue();
        }

        private void _getPinDirection()
        {
            PinDirection = ioService.Gpio.GetPinMode(pinLocation);
        }

        private void _getPinValue()
        {
            PinState = ioService.Gpio.Read(pinLocation);
        }

    }
}
