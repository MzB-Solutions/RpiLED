using RpiLed.Core;
using RpiLED.Core.Models;
using System;
using System.Data;
using System.Threading;

namespace RpiLED.Core.Services
{
    public class ShiftRegisterService
    {
        #region Private Fields

        // Storage Register clock
        private static readonly int RCLK = 18;

        // Serial Data In Pin
        private static readonly int SDI = 16;

        // Shift register clock
        private static readonly int SRCLK = 29;

        private readonly PinModel sdiPin = new(SDI, PinService.Gpio);

        private readonly PinModel srclkPin = new(SRCLK, PinService.Gpio);

        #endregion Private Fields

        #region Private Destructors

        ~ShiftRegisterService()
        {
            sdiPin.gpioService.Gpio.Dispose();
            rclkPin.gpioService.Gpio.Dispose();
            srclkPin.gpioService.Gpio.Dispose();
        }

        #endregion Private Destructors

        #region Private Methods

        private static int GetBitPattern(char character)
        {
            return character switch
            {
                '0' => (int)DisplayCharactersEnum.HexZero,
                '1' => (int)DisplayCharactersEnum.HexOne,
                '2' => (int)DisplayCharactersEnum.HexTwo,
                '3' => (int)DisplayCharactersEnum.HexThree,
                '4' => (int)DisplayCharactersEnum.HexFour,
                '5' => (int)DisplayCharactersEnum.HexFive,
                '6' => (int)DisplayCharactersEnum.HexSix,
                '7' => (int)DisplayCharactersEnum.HexSeven,
                '8' => (int)DisplayCharactersEnum.HexEight,
                '9' => (int)DisplayCharactersEnum.HexNine,
                'A' => (int)DisplayCharactersEnum.HexA,
                'B' => (int)DisplayCharactersEnum.HexB,
                'C' => (int)DisplayCharactersEnum.HexC,
                'D' => (int)DisplayCharactersEnum.HexD,
                'E' => (int)DisplayCharactersEnum.HexE,
                'F' => (int)DisplayCharactersEnum.HexF,
                'a' => (int)DisplayCharactersEnum.HexA,
                'b' => (int)DisplayCharactersEnum.HexB,
                'c' => (int)DisplayCharactersEnum.HexC,
                'd' => (int)DisplayCharactersEnum.HexD,
                'e' => (int)DisplayCharactersEnum.HexE,
                'f' => (int)DisplayCharactersEnum.HexF,
                _ => (int)DisplayCharactersEnum.None
            };
        }

        private void SI(byte ch)
        {
            for (var i = 0; i < 8; i++)
            {
                sdiPin.PinWrite((ch & (0x80 >> i)) > 0);
                Pulse(srclkPin);
            }
        }

        #endregion Private Methods

        #region Public Fields

        public static readonly byte[] ShiftOutput =
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

        public readonly PinModel rclkPin = new(RCLK, PinService.Gpio);

        #endregion Public Fields

        #region Public Constructors

        public ShiftRegisterService()
        {
            sdiPin.PinWrite(false);
            rclkPin.PinWrite(false);
            srclkPin.PinWrite(false);
            Pulse(rclkPin);
            Thread.Sleep(100);
        }

        #endregion Public Constructors

        #region Public Methods

        public static void Pulse(PinModel pin)
        {
            pin.PinWrite(false);
            Thread.Sleep(50);
            pin.PinWrite(true);
        }

        public void RunTest()
        {
            for (var i = 0; i < 8; i++)
            {
                SI(ShiftOutput[i]);
                Pulse(rclkPin);
                Thread.Sleep(150);
            }

            Thread.Sleep(500);
            for (var i = 0; i < 3; i++)
            {
                SI(0xff);
                Pulse(rclkPin);
                Thread.Sleep(100);
                SI(0x00);
                Thread.Sleep(100);
            }

            Thread.Sleep(500);
            for (var i = 0; i < 8; i++)
            {
                SI(ShiftOutput[8 - i - 1]);
                Pulse(rclkPin);
                Thread.Sleep(150);
            }

            Thread.Sleep(500);
            for (var i = 0; i < 3; i++)
            {
                SI(0xff);
                Pulse(rclkPin);
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
                sdiPin.PinWrite(Convert.ToBoolean(val));
                Pulse(srclkPin);
            }

            Console.WriteLine(@"]");

            Pulse(rclkPin);
            Thread.Sleep(100);
        }

        #endregion Public Methods
    }
}