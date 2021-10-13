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
            sdiPin.PinWrite(false);
            rclkPin.PinWrite(false);
            srclkPin.PinWrite(false);
            _pulse(rclkPin);
            Thread.Sleep(100);
        }

        #endregion Public Constructors

        #region Private Destructors

        ~ShiftRegisterService()
        {
        }

        #endregion Private Destructors

        #region Public Methods

        public void ShiftIn(char c)
        {
            var pattern = GetBitPattern(c);
            if (pattern == 0b00000000)
            {
                throw new InvalidExpressionException("This is not a valid HEX character!");
            }
            Console.WriteLine($@"This is the byte sequence we're gonna send : "+Convert.ToString(pattern, 2));
            Console.Write(@"Writing : [");
            for (var i = 0; i < 8; i++)
            {
                var val = (pattern & (0x80 >> i));
                Console.Write(Convert.ToString(val, 2));
                sdiPin.PinWrite(val);
                _pulse(srclkPin);
            }
            Console.WriteLine(@"]");

            _pulse(rclkPin);
            Thread.Sleep(100);
        }

        #endregion Public Methods

        #region Private Fields

        // Storage Register clock
        private static readonly int RCLK = 18;

        // Serial Data In Pin
        private static readonly int SDI = 16;

        // Shift register clock
        private static readonly int SRCLK = 29;

        private readonly PinModel rclkPin = new(RCLK, PinService.Gpio);

        private readonly PinModel sdiPin = new(SDI, PinService.Gpio);

        private readonly PinModel srclkPin = new(SRCLK, PinService.Gpio);

        #endregion Private Fields

        #region Private Methods

        private void _pulse(PinModel pin)
        {
            pin.PinWrite(false);
            Thread.Sleep(50);
            pin.PinWrite(true);
        }

        private int GetBitPattern(char character)
        {
            switch (character)
            {
                case '0':
                    return (int)DisplayCharactersEnum.HexZero;

                case '1':
                    return (int)DisplayCharactersEnum.HexOne;

                case '2':
                    return (int)DisplayCharactersEnum.HexTwo;

                case '3':
                    return (int)DisplayCharactersEnum.HexThree;

                case '4':
                    return (int)DisplayCharactersEnum.HexFour;

                case '5':
                    return (int)DisplayCharactersEnum.HexFive;

                case '6':
                    return (int)DisplayCharactersEnum.HexSix;

                case '7':
                    return (int)DisplayCharactersEnum.HexSeven;

                case '8':
                    return (int)DisplayCharactersEnum.HexEight;

                case '9':
                    return (int)DisplayCharactersEnum.HexNine;

                case 'a':
                case 'A':
                    return (int)DisplayCharactersEnum.HexA;

                case 'b':
                case 'B':
                    return (int)DisplayCharactersEnum.HexB;

                case 'c':
                case 'C':
                    return (int)DisplayCharactersEnum.HexC;

                case 'd':
                case 'D':
                    return (int)DisplayCharactersEnum.HexD;

                case 'e':
                case 'E':
                    return (int)DisplayCharactersEnum.HexE;

                case 'f':
                case 'F':
                    return (int)DisplayCharactersEnum.HexF;

                default:
                    return (int)DisplayCharactersEnum.None;
            }
        }

        #endregion Private Methods
    }
}