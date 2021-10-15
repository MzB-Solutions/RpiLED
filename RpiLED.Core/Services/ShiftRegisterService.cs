using System;
using System.Data;
using System.Threading;
using RpiLed.Core;
using RpiLED.Core.Models;

namespace RpiLED.Core.Services
{
    public class ShiftRegisterService
    {
        #region Public Constructors

        public ShiftRegisterService()
        {
            _sdiPin.PinWrite(false);
            _rclkPin.PinWrite(false);
            _srclkPin.PinWrite(false);
            // Reset pin is active low!!
            _rstPin.PinWrite(true);
        }

        #endregion Public Constructors

        #region Private Destructors

        ~ShiftRegisterService()
        {
            _sdiPin.gpioService.Gpio.Dispose();
            _rclkPin.gpioService.Gpio.Dispose();
            _srclkPin.gpioService.Gpio.Dispose();
            _rstPin.gpioService.Gpio.Dispose();
        }

        #endregion Private Destructors

        #region Private Fields

        // Storage Register clock (ST_CP)
        private const int Rclk = 18;

        // Serial Data In  (DS) Pin
        private const int Sdi = 16;

        // Reset (MR) Pin
        private const int Rst = 22;

        // Shift register clock (SH_CP)
        private const int Srclk = 29;

        // Test Led Pin
        private const int IndLed = 13;

        private static readonly byte[] ShiftOutput =
        {
            0x01,
            0x02,
            0x04,
            0x08,
            0x10,
            0x20,
            0x40,
            0x80
        };

        private readonly PinModel _indPin = new(IndLed, PinService.Gpio);
        private readonly PinModel _rclkPin = new(Rclk, PinService.Gpio);
        private readonly PinModel _sdiPin = new(Sdi, PinService.Gpio);
        private readonly PinModel _rstPin = new(Rst, PinService.Gpio);
        private readonly PinModel _srclkPin = new(Srclk, PinService.Gpio);

        #endregion Private Fields

        #region Private Methods

        private void ResetSr()
        {
            _rstPin.PinWrite(false);
            Pulse(_rclkPin);
            Pulse(_srclkPin);
            _rstPin.PinWrite(true);
        }

        private static string ToBinary(int myValue)
        {
            var binVal = Convert.ToString(myValue, 2);
            var bits = 0;
            var bitblock = 4;

            for (var i = 0; i < binVal.Length; i += bitblock)
            { bits += bitblock; }

            return binVal.PadLeft(bits, '0');
        }

        private static byte GetBitPattern(char character)
        {
            return character switch
            {
                '0' => (byte)DisplayCharactersEnum.HexZero,
                '1' => (byte)DisplayCharactersEnum.HexOne,
                '2' => (byte)DisplayCharactersEnum.HexTwo,
                '3' => (byte)DisplayCharactersEnum.HexThree,
                '4' => (byte)DisplayCharactersEnum.HexFour,
                '5' => (byte)DisplayCharactersEnum.HexFive,
                '6' => (byte)DisplayCharactersEnum.HexSix,
                '7' => (byte)DisplayCharactersEnum.HexSeven,
                '8' => (byte)DisplayCharactersEnum.HexEight,
                '9' => (byte)DisplayCharactersEnum.HexNine,
                'A' => (byte)DisplayCharactersEnum.HexA,
                'B' => (byte)DisplayCharactersEnum.HexB,
                'C' => (byte)DisplayCharactersEnum.HexC,
                'D' => (byte)DisplayCharactersEnum.HexD,
                'E' => (byte)DisplayCharactersEnum.HexE,
                'F' => (byte)DisplayCharactersEnum.HexF,
                'a' => (byte)DisplayCharactersEnum.HexA,
                'b' => (byte)DisplayCharactersEnum.HexB,
                'c' => (byte)DisplayCharactersEnum.HexC,
                'd' => (byte)DisplayCharactersEnum.HexD,
                'e' => (byte)DisplayCharactersEnum.HexE,
                'f' => (byte)DisplayCharactersEnum.HexF,
                _ => (byte) DisplayCharactersEnum.None
            };
        }

        private void SI(byte ch)
        {
            Console.Write(@"SerialOutput[");
            for (var i = 0; i < 8; i++)
            {
                var val = (ch & (0x80 >> i)) > 0;
                Console.Write(i+@":"+Convert.ToInt16(val));
                _sdiPin.PinWrite(val);
                _indPin.PinWrite(val);
                Thread.Sleep(20);
                Pulse(_srclkPin);
            }
            Console.WriteLine(@"]");
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Send a triple of either 0/1/0 to a pin or 1/0/1 (in case of <see cref="inverted">inverted</see>=true)
        /// </summary>
        /// <param name="pin">Some <see cref="PinModel">Pinmodel</see> instantiated GPIO pin</param>
        /// <param name="inverted">This parameter allows us to interact with active_low pins, whose default behaviour is to be always enabled and thus need an off pulse to act</param>
        /// <remarks>We're using timespan here to get near nano-second accuracy for pulsing (as close as we need it to be within 500 nanos)</remarks>
        private static void Pulse(PinModel pin, bool inverted = false)
        {
            pin.PinWrite(inverted);
            Thread.Sleep(TimeSpan.FromMilliseconds(0.5d));
            pin.PinWrite(!inverted);
            Thread.Sleep(TimeSpan.FromMilliseconds(0.5d));
            pin.PinWrite(inverted);
            Thread.Sleep(TimeSpan.FromMilliseconds(0.5d));
        }

        public void RunTest()
        {
            ResetSr();
            Console.WriteLine(@"Test1");
            for (var i = 0; i < 8; i++)
            {
                SI(ShiftOutput[i]);
                Pulse(_rclkPin);
                Thread.Sleep(200);
            }
            Console.WriteLine(@"Reset");
            Thread.Sleep(500);
            for (var i = 0; i < 4; i++)
            {
                SI(0xff);
                Pulse(_rclkPin);
                Thread.Sleep(200);
                SI(0x00);
                Thread.Sleep(200);
            }
            Console.WriteLine(@"Test2");
            Thread.Sleep(500);
            for (var i = 0; i < 8; i++)
            {
                SI(ShiftOutput[8 - i - 1]);
                Pulse(_rclkPin);
                Thread.Sleep(200);
            }
            Console.WriteLine(@"Reset");
            Thread.Sleep(500);
            for (var i = 0; i < 4; i++)
            {
                SI(0xff);
                Pulse(_rclkPin);
                Thread.Sleep(200);
                SI(0x00);
                Thread.Sleep(200);
            }
            ResetSr();
            Console.WriteLine($@"testing HexA");
            SI((byte)DisplayCharactersEnum.HexA);
            Thread.Sleep(1000);
            ResetSr();
            Console.WriteLine($@"testing Hex0");
            SI((byte)DisplayCharactersEnum.HexZero);
            Thread.Sleep(1000);
            ResetSr();
            Console.WriteLine($@"testing Hex8");
            SI((byte)DisplayCharactersEnum.HexEight);
            Thread.Sleep(1000);
            ResetSr();
        }

        public void ShiftIn(char c)
        {
            ResetSr();
            var stringPattern = ToBinary(GetBitPattern(c));
            Console.WriteLine(@"This is the byte sequence we're gonna send : " + stringPattern);
            if (stringPattern.Length < 8)
            {
                throw new AccessViolationException($@"Not enough bits supplied (found:{stringPattern.Length} bits, require 8 bits)");
            }
            Console.Write(@"Writing : [");
            for (var i = 0; i <= 7; i++)
            {
                //var val = pattern & Convert.ToInt16(0x80 >> i > 0);
                var singleValue = stringPattern[i].ToString();
                var val = Convert.ToBoolean(int.Parse(singleValue));
                Console.Write(i + @":" + singleValue+@"|");
                _sdiPin.PinWrite(val);
                Pulse(_srclkPin);
            }
            Console.WriteLine(@"]");
            Thread.Sleep(100);
            Pulse(_rclkPin);
        }

        #endregion Public Methods
    }
}