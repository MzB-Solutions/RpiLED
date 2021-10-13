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
        }

        #endregion Public Constructors

        #region Private Destructors

        ~ShiftRegisterService()
        {
            _sdiPin.gpioService.Gpio.Dispose();
            _rclkPin.gpioService.Gpio.Dispose();
            _srclkPin.gpioService.Gpio.Dispose();
        }

        #endregion Private Destructors

        #region Private Fields

        // Storage Register clock
        private const int Rclk = 18;

        // Serial Data In Pin
        private const int Sdi = 16;

        // Shift register clock
        private const int Srclk = 29;

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

        private readonly PinModel _rclkPin = new(Rclk, PinService.Gpio);
        private readonly PinModel _sdiPin = new(Sdi, PinService.Gpio);

        private readonly PinModel _srclkPin = new(Srclk, PinService.Gpio);

        #endregion Private Fields

        #region Private Methods

        private static int GetBitPattern(char character)
        {
            return character switch
            {
                '0' => (int) DisplayCharactersEnum.HexZero,
                '1' => (int) DisplayCharactersEnum.HexOne,
                '2' => (int) DisplayCharactersEnum.HexTwo,
                '3' => (int) DisplayCharactersEnum.HexThree,
                '4' => (int) DisplayCharactersEnum.HexFour,
                '5' => (int) DisplayCharactersEnum.HexFive,
                '6' => (int) DisplayCharactersEnum.HexSix,
                '7' => (int) DisplayCharactersEnum.HexSeven,
                '8' => (int) DisplayCharactersEnum.HexEight,
                '9' => (int) DisplayCharactersEnum.HexNine,
                'A' => (int) DisplayCharactersEnum.HexA,
                'B' => (int) DisplayCharactersEnum.HexB,
                'C' => (int) DisplayCharactersEnum.HexC,
                'D' => (int) DisplayCharactersEnum.HexD,
                'E' => (int) DisplayCharactersEnum.HexE,
                'F' => (int) DisplayCharactersEnum.HexF,
                'a' => (int) DisplayCharactersEnum.HexA,
                'b' => (int) DisplayCharactersEnum.HexB,
                'c' => (int) DisplayCharactersEnum.HexC,
                'd' => (int) DisplayCharactersEnum.HexD,
                'e' => (int) DisplayCharactersEnum.HexE,
                'f' => (int) DisplayCharactersEnum.HexF,
                _ => (int) DisplayCharactersEnum.None
            };
        }

        private void SI(byte ch)
        {
            for (var i = 0; i < 8; i++)
            {
                var val = (ch & (0x80 >> i)) > 0;
                Console.WriteLine(@"SerialOutput["+i+@"]:"+val);
                _sdiPin.PinWrite(val);
                Pulse(_srclkPin);
            }
        }

        #endregion Private Methods

        #region Public Methods

        private static void Pulse(PinModel pin)
        {
            Console.WriteLine(@"Pulsing Pin:"+pin.GetType());
            pin.PinWrite(false);
            Thread.Sleep(50);
            pin.PinWrite(true);
            Thread.Sleep(50);
            pin.PinWrite(false);
        }

        public void RunTest()
        {
            Console.WriteLine(@"Test1");
            for (var i = 0; i < 8; i++)
            {
                SI(ShiftOutput[i]);
                Pulse(_rclkPin);
                Thread.Sleep(150);
            }
            Console.WriteLine(@"Reset");
            Thread.Sleep(500);
            for (var i = 0; i < 3; i++)
            {
                SI(0xff);
                Pulse(_rclkPin);
                Thread.Sleep(100);
                SI(0x00);
                Thread.Sleep(100);
            }
            Console.WriteLine(@"Test2");
            Thread.Sleep(500);
            for (var i = 0; i < 8; i++)
            {
                SI(ShiftOutput[8 - i - 1]);
                Pulse(_rclkPin);
                Thread.Sleep(150);
            }
            Console.WriteLine(@"Reset");
            Thread.Sleep(500);
            for (var i = 0; i < 3; i++)
            {
                SI(0xff);
                Pulse(_rclkPin);
                Thread.Sleep(100);
                SI(0x00);
                Thread.Sleep(100);
            }
        }

        public void ShiftIn(char c)
        {
            var pattern = GetBitPattern(c);
            if (pattern == 0b00000000) throw new InvalidExpressionException("This is not a valid HEX character!");
            Console.WriteLine(@"This is the byte sequence we're gonna send : " + Convert.ToString(pattern, 2));
            Console.Write(@"Writing : [");
            for (var i = 0; i < 8; i++)
            {
                var val = pattern & Convert.ToInt16(0x80 >> i > 1);
                Console.Write(Convert.ToString(val, 2) + @"|" + Convert.ToBoolean(val));
                _sdiPin.PinWrite(Convert.ToBoolean(val));
                Pulse(_srclkPin);
            }

            Console.WriteLine(@"]");

            Pulse(_rclkPin);
            Thread.Sleep(100);
        }

        #endregion Public Methods
    }
}