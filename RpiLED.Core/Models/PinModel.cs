using RpiLed.Core;
using RpiLed.Core.Services;
using RpiLED.Core.Services;
using System;
using System.Device.Gpio;
using System.IO;
using System.Linq;

namespace RpiLED.Core.Models
{
    public class PinModel
    {
        #region Private Fields

        private static int _pinLocation;

        private PinService ourService;

        #endregion Private Fields

        #region Private Destructors

        ~PinModel()
        {
            switch (OurService)
            {
                case PinService.Gpio:
                    gpioService.Gpio.ClosePin(_pinLocation);
                    break;

                case PinService.Pwm:
                    pwmService.Pwm.Stop();
                    pwmService.Pwm.Dispose();
                    break;

                default:
                    break;
            }
        }

        #endregion Private Destructors

        #region Private Methods

        private void GetPinDirection()
        {
            PinDirection = gpioService.Gpio.GetPinMode(_pinLocation);
        }

        private void GetPinValue()
        {
            PinState = gpioService.Gpio.Read(_pinLocation);
        }

        private bool IsValidPin(int pin)
        {
            return gpioService.ValidPins.Contains((Pins)pin);
        }

        private void ResetPin(bool openForWrite = false)
        {
            if (IsValidPin(_pinLocation))
                gpioService.Gpio.OpenPin(_pinLocation, openForWrite ? PinMode.Output : PinMode.Input);
            else
                throw new IOException("You cannot use that pin, since it is part of the power-rail!");
        }

        #endregion Private Methods

        #region Protected Internal Properties

        protected internal PinService OurService
        {
            get { return ourService; }
            set { ourService = value; }
        }

        #endregion Protected Internal Properties

        #region Internal Fields

        /// <summary>
        ///     This represents a handler on a GPIO service
        /// </summary>
        internal GpioService gpioService;

        /// <summary>
        ///     This is a handle on a PWM service
        /// </summary>
        internal PwmService pwmService;

        #endregion Internal Fields

        #region Public Constructors

        public PinModel(int pinNumber, PinService srv)
        {
            switch (srv)
            {
                case PinService.Gpio:
                    gpioService = new GpioService();
                    break;

                case PinService.Pwm:
                    pwmService = new PwmService((PwmSelect)pinNumber);
                    break;

                default:
                    throw new NotImplementedException("This functionality is not implemented yet.");
            }

            _pinLocation = pinNumber;
            ResetPin();
            GetPinDirection();
            GetPinValue();
            gpioService.Gpio.ClosePin(_pinLocation);
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        ///     Input or Output
        /// </summary>
        public PinMode PinDirection { get; private set; }

        /// <summary>
        ///     Current Value on the pin
        /// </summary>
        /// <remarks>
        ///     This is either a boolean, in case of GPIO, or a double value between 0 and 1, for PWM
        /// </remarks>
        public PinValue PinState { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void PinWrite(bool value)
        {
            ResetPin(true);
            gpioService.Gpio.Write(_pinLocation, value);
            GetPinDirection();
            GetPinValue();
            gpioService.Gpio.ClosePin(_pinLocation);
        }

        public void PinWrite(double value)
        {
            ResetPin(true);
            pwmService.Pwm.DutyCycle = value;
            GetPinDirection();
            GetPinValue();
            pwmService.Pwm.Start();
        }

        #endregion Public Methods
    }
}