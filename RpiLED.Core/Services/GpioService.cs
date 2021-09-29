using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using RpiLed.Core;

namespace RpiLED.Core.Services
{
    public enum PinDirection
    {
        In,
        Out
    }

    public class GpioService
    {
        #region Private Fields

        /// <summary>
        ///     These Pins should be excluded from any consideration! They are +3 Volts DC!
        /// </summary>
        private readonly List<Pins> _3VPins = new() { Pins.P01, Pins.P17 };

        /// <summary>
        ///     These Pins should be excluded from any consideration! They are +5 Volts DC!
        /// </summary>
        private readonly List<Pins> _5VPins = new() { Pins.P02, Pins.P04 };

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

        #endregion Private Fields

        #region Private Methods

        /// <summary>
        ///     This sets the pin numberring scheme. Hardcoded to physical pin-numbering.
        /// </summary>
        private void SetGpioScheme()
        {
            Gpio = new GpioController(PinNumberingScheme.Board);
        }

        #endregion Private Methods

        #region Public Constructors

        /// <summary>
        ///     The gpioService Constructor
        /// </summary>
        /// <remarks>
        ///     Here we invalidate certain pins since they are part of the power rail.
        ///     In the PinModel, we should always check wether the pin we are talking to,
        ///     is contained in the ValidPins IEnumerable list
        ///     We also set the Pinscheme to the physical pin representation.
        /// </remarks>
        public GpioService()
        {
            _forbiddenPins.AddRange(_groundPins);
            _forbiddenPins.AddRange(_3VPins);
            _forbiddenPins.AddRange(_5VPins);
            ValidPins = _availablePins.Except(_forbiddenPins);
            SetGpioScheme();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        ///     An instance of an actual GPIO controller
        /// </summary>
        public GpioController Gpio { get; private set; }

        /// <summary>
        ///     This (when) filled contains only valid pins, meaning no power rails, or ground rails.
        /// </summary>
        /// <value>Contains a list of valid pins.</value>
        public IEnumerable<Pins> ValidPins { get; }

        #endregion Public Properties
    }
}