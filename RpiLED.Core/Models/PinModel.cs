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
    public class PinModel
    {
        internal GpioService ioService;

        private static int pinLocation;

        public PinValue PinState { get; private set; }
        public PinMode PinDirection { get; private set; }

        private void _resetPin(bool openForWrite = false)
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

        public PinModel(PinScheme scheme, int pinNumber)
        {
            ioService = new GpioService();
            pinLocation = pinNumber;
            _resetPin();
            _getPinValue();
        }

        ~PinModel()
        {
            ioService.Gpio.ClosePin(pinLocation);
        }

        public void PinWrite(bool value)
        {
            _resetPin(true);
            ioService.Gpio.Write(pinLocation, value);
            _getPinDirection();
            _getPinValue();
        }

        private void _setPinDirection(PinMode mode)
        {
            ioService.Gpio.SetPinMode(pinLocation, mode);
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
