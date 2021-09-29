using System;
using System.Collections.Generic;
using System.Device.Pwm;
using System.Device.Pwm.Drivers;
using System.Linq;
using RpiLed.Core.Exceptions;
using RpiLed.Core.Properties;

namespace RpiLed.Core.Services
{
    internal enum PwmSelect
    {
        Pin12 = 12,
        Pin32 = 32,
        Pin33 = 33,
        Pin35 = 35
    }

    internal class PwmService
    {
        #region Public Fields

        public PwmChannel Pwm;

        #endregion Public Fields

        #region Public Constructors

        /// <summary>
        ///     The Constructor for the PwmService
        ///     this automatically decides which PWM implementation to use (ie: HW or SW supported pwm service)
        /// </summary>
        /// <seealso cref="_hwPwmPins" />
        /// <param name="selectedPin">An int (hopefully) with the physical pin we are adressing</param>
        public PwmService(PwmSelect selectedPin)
        {
            // Generate a complete list of forbidden pins
            _forbiddenPins.AddRange(_groundPins);
            _forbiddenPins.AddRange(_3VPins);
            _forbiddenPins.AddRange(_5VPins);
            // from all available pins take away the forbidden pins
            _availablePins = (List<Pins>)_availablePins.Except(_forbiddenPins);
            // create list of sw pins by subtracting the hw pin list from available pins
            _swPwmPins = (List<Pins>)_availablePins.Except(_hwPwmPins);
            //logic to select chip and channel on SOC
            if (_hwPwmPins.Contains((Pins)selectedPin))
            {
                var chip = 0;
                var channel = 0;
                PwmSelect((int)selectedPin, ref chip, ref channel);
                Pwm = PwmChannel.Create(chip, channel, 400, 0);
            }
            else if (_swPwmPins.Contains((Pins)selectedPin))
            {
                Pwm = new SoftwarePwmChannel((int)selectedPin, 400, 0, shouldDispose: true);
            }
            else
            {
                throw new EPinNotValidException(string.Format(ExceptionMessages.PinNotValidMsg,
                    (int)selectedPin));
            }
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        ///     Figure out depending on the physical pin which chip and channel
        ///     to select (assuming HW support for that pin of course)
        /// </summary>
        /// <remarks>
        ///     please check hardware documentation for further information
        /// </remarks>
        /// <param name="pin">The physical pin selected for pwm service</param>
        /// <param name="chip">Either 0 or 1</param>
        /// <param name="channel">Either 0 or 1</param>
        private static void PwmSelect(int pin, ref int chip, ref int channel)
        {
            switch (pin)
            {
                case 12:
                    chip = 0;
                    channel = 0;
                    break;

                case 32:
                    chip = 0;
                    channel = 1;
                    break;

                case 33:
                    chip = 1;
                    channel = 0;
                    break;

                case 35:
                    chip = 1;
                    channel = 1;
                    break;
            }
        }

        #endregion Private Methods

        #region Private Fields

        /// <summary>
        ///     These Pins should be excluded from any consideration! They are +3 Volts DC!
        /// </summary>
        private readonly List<Pins> _3VPins = new()
        { Pins.P01, Pins.P17 };

        /// <summary>
        ///     These Pins should be excluded from any consideration! They are +5 Volts DC!
        /// </summary>
        private readonly List<Pins> _5VPins = new()
        { Pins.P02, Pins.P04 };

        /// <summary>
        ///     This is a list of ALL available pins (40) on the board
        /// </summary>
        private readonly List<Pins> _availablePins =
            Enum.GetValues(typeof(Pins))
                .Cast<Pins>()
                .ToList();

        /// <summary>
        ///     This when filled contains only non-valid pins, meaning this contains the following groups:
        ///     2x +3.3v | 2x +5v | 8x Ground
        /// </summary>
        /// <returns>A list of forbidden pins.</returns>
        private readonly List<Pins> _forbiddenPins = new();

        /// <summary>
        ///     These Pins should be excluded from any consideration! They are ground!
        /// </summary>
        private readonly List<Pins> _groundPins = new()
        {
            Pins.P06,
            Pins.P09,
            Pins.P14,
            Pins.P20,
            Pins.P25,
            Pins.P30,
            Pins.P34,
            Pins.P39
        };

        /// <summary>
        ///     This is the list of HW supported PWM pins
        /// </summary>
        /// <remarks>
        ///     This increases the accuracy of the PWM frequency, to up to 400Hz (afaik)
        ///     However, there is only 4 of them
        /// </remarks>
        private readonly List<Pins> _hwPwmPins = new()
        {
            Pins.P12,
            Pins.P18,
            Pins.P33,
            Pins.P35
        };

        /// <summary>
        ///     This field contains a list of software supported PWM pins
        /// </summary>
        /// <remarks>
        ///     As opposed to the hw implementation, the highest "reliable" frequency is
        ///     capped at  roughly 100Hz
        ///     As you may note, this is empty at compile time, but gets filled during
        ///     construction at runtime of the PwmService..
        /// </remarks>
        private readonly List<Pins> _swPwmPins;

        #endregion Private Fields
    }
}