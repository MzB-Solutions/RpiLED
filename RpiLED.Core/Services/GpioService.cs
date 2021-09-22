using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Gpio;

namespace RpiLED.Core.Services
{
    internal enum Pins
    {
        P01 = 1,
        P02 = 2,
        P03 = 3,
        P04 = 4,
        P05 = 5,
        P06 = 6,
        P07 = 7,
        P08 = 8,
        P09 = 9,
        P10 = 10,
        P11 = 11,
        P12 = 12,
        P13 = 13,
        P14 = 14,
        P15 = 15,
        P16 = 16,
        P17 = 17,
        P18 = 18,
        P19 = 19,
        P20 = 20,
        P21 = 21,
        P22 = 22,
        P23 = 23,
        P24 = 24,
        P25 = 25,
        P26 = 26,
        P27 = 27,
        P28 = 28,
        P29 = 29,
        P30 = 30,
        P31 = 31,
        P32 = 32,
        P33 = 33,
        P34 = 34,
        P35 = 35,
        P36 = 36,
        P37 = 37,
        P38 = 38,
        P39 = 39,
        P40 = 40
    };

    public enum PinScheme
    {
        Physical = 1,
        Broadcom = 2,
        None = -1
    };

    public enum PinDirection
    {
        In,
        Out
    };

    public enum PinMode
    {
        Gpio = 1,
        Pwm = 2,
        None = -1
    };

    public class GpioService
    {
        /// <summary>
        /// This is a list of ALL available pins (40) on the board
        /// </summary>
        internal List<Pins> AvailablePins = new()
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

        internal List<Pins> _forbiddenPins = new();

        /// <summary>
        /// These Pins should be excluded from any consideration! They are ground!
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
        /// These Pins should be excluded from any consideration! They are +3 Volts DC!
        /// </summary>
        private readonly List<Pins> _3vPins = new() { Pins.P01, Pins.P17 };

        /// <summary>
        /// These Pins should be excluded from any consideration! They are +5 Volts DC!
        /// </summary>
        private readonly List<Pins> _5vPins = new() { Pins.P02, Pins.P04 };

        public GpioController Gpio { get; private set; }

        private void SetScheme()
        {
            Gpio = new GpioController(PinNumberingScheme.Board);
        }

        public GpioService()
        {
            _forbiddenPins.AddRange(_groundPins);
            _forbiddenPins.AddRange(_3vPins);
            _forbiddenPins.AddRange(_5vPins);
            Console.WriteLine("ALL Pins " + AvailablePins + " !!!");
            AvailablePins.Intersect(_forbiddenPins);
            Console.WriteLine("FORBIDDEN Pins " + _forbiddenPins + " !!!");
            Console.WriteLine("Our Available Pins " + AvailablePins + " !!!");
            SetScheme();
        }
    }
}
