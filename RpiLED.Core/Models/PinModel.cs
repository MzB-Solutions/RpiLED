using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RpiLED.Core.Services;
using PinMode = System.Device.Gpio.PinMode;

namespace RpiLED.Core.Models
{
    public class PinModel
    {
        internal GpioService ioService;

        private static int pinLocation;

        public PinValue PinState { get; private set; }
        public PinMode PinDirection { get; private set; }

        private bool IsValidPin(int pin)
        {
            if (ioService.ValidPins.Contains((Pins)pin))
            {
                var result = $@"Found pin {pin} in {ioService.ValidPins.ToString()}";
                Console.WriteLine(result);
                return true;
            }
            else
            {
                var result = $@"You cannot use that pin ({pin}), since it is part of the power-rail!";
                Console.WriteLine(result);
                return false;
            }
        }

        private void _resetPin(bool openForWrite = false)
        {

            if (IsValidPin(pinLocation))
            {
                if (openForWrite)
                {
                    ioService.Gpio.OpenPin(pinLocation, PinMode.Output);
                }
                else
                {
                    ioService.Gpio.OpenPin(pinLocation, PinMode.Input);
                }
            }
            else
            {
                throw new IOException("You cannot use that pin, since it is part of the power-rail!");
            }

        }

        public PinModel(PinScheme scheme, int pinNumber)
        {
            ioService = new GpioService();
            pinLocation = pinNumber;
            _resetPin();
            _getPinDirection();
            _getPinValue();
            ioService.Gpio.ClosePin(pinLocation);
        }

        public void PinWrite(bool value)
        {
            _resetPin(true);
            ioService.Gpio.Write(pinLocation, value);
            _getPinDirection();
            _getPinValue();
            ioService.Gpio.ClosePin(pinLocation);
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
