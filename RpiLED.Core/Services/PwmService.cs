using System.Collections.Generic;
using System.Device.Pwm;
using System.Device.Pwm.Drivers;

namespace RpiLed.Core.Services
{
    internal enum PwmMode
    {
        Hardware,
        Software
    }

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

        public PwmService(PwmSelect selectedPin, PwmMode mode)
        {
            if (mode == PwmMode.Hardware)
            {
                var chip = 0;
                var channel = 0;
                pwmSelect((int) selectedPin, ref chip, ref channel);
                Pwm = PwmChannel.Create(chip, channel, 400, 0);
            }
            else
            {
                Pwm = new SoftwarePwmChannel((int) selectedPin, 400, 0, shouldDispose: true);
            }
        }

        #endregion Public Constructors

        #region Private Methods

        private void pwmSelect(int pin, ref int chip, ref int channel)
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
        private readonly List<Pins> _3vPins = new()
            {Pins.P01, Pins.P17};

        /// <summary>
        ///     These Pins should be excluded from any consideration! They are +5 Volts DC!
        /// </summary>
        private readonly List<Pins> _5vPins = new()
            {Pins.P02, Pins.P04};

        /// <summary>
        ///     This is a list of ALL available pins (40) on the board
        /// </summary>
        private readonly List<Pins> _availablePins = new()
        {
            Pins.P01,
            Pins.P02,
            Pins.P03,
            Pins.P04,
            Pins.P05,
            Pins.P06,
            Pins.P07,
            Pins.P08,
            Pins.P09,
            Pins.P10,
            Pins.P11,
            Pins.P12,
            Pins.P13,
            Pins.P14,
            Pins.P15,
            Pins.P16,
            Pins.P17,
            Pins.P18,
            Pins.P19,
            Pins.P20,
            Pins.P21,
            Pins.P22,
            Pins.P23,
            Pins.P24,
            Pins.P25,
            Pins.P26,
            Pins.P27,
            Pins.P28,
            Pins.P29,
            Pins.P30,
            Pins.P31,
            Pins.P32,
            Pins.P33,
            Pins.P34,
            Pins.P35,
            Pins.P36,
            Pins.P37,
            Pins.P38,
            Pins.P39,
            Pins.P40
        };

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

        private List<Pins> PwmSelect = new()
        {
            Pins.P12,
            Pins.P32,
            Pins.P33,
            Pins.P35
        };

        #endregion Private Fields
    }
}