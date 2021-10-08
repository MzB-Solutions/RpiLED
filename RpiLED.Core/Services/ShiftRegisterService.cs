using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using RpiLED.Core.Models;

namespace RpiLed.Core.Services
{
    public class ShiftRegisterService
    {
        public List<DisplayCharactersEnum> Characters =
        Enum.GetValues(typeof(DisplayCharactersEnum))
        .Cast<DisplayCharactersEnum>()
        .ToList();

        // Serial Data In Pin
        private static int SDI = 16;

        private PinModel sdiPin = new PinModel(SDI, PinService.Gpio);
        // Storage Register clock
        private static int RCLK = 18;

        private PinModel rclkPin = new PinModel(RCLK, PinService.Gpio);
        // Shift register clock
        private static int SRCLK = 29;
        private PinModel srclkPin = new PinModel(SRCLK, PinService.Gpio);

        private void _pulse(PinModel pin)
        {
            pin.PinWrite(false);
            pin.PinWrite(true);
        }

        private dynamic GetBitPattern(char character)
        {
            switch (character)
            {
                case '0':
                    return DisplayCharactersEnum.HexZero;
                case '1':
                    return DisplayCharactersEnum.HexOne;
                case '2':
                    return DisplayCharactersEnum.HexTwo;
                case '3':
                    return DisplayCharactersEnum.HexThree;
                case '4':
                    return DisplayCharactersEnum.HexFour;
                case '5':
                    return DisplayCharactersEnum.HexFive;
                case '6':
                    return DisplayCharactersEnum.HexSix;
                case '7':
                    return DisplayCharactersEnum.HexSeven;
                case '8':
                    return DisplayCharactersEnum.HexEight;
                case '9':
                    return DisplayCharactersEnum.HexNine;
                case 'a':
                case 'A':
                    return DisplayCharactersEnum.HexA;
                case 'b':
                case 'B':
                    return DisplayCharactersEnum.HexB;
                case 'c':
                case 'C':
                    return DisplayCharactersEnum.HexC;
                case 'd':
                case 'D':
                    return DisplayCharactersEnum.HexD;
                case 'e':
                case 'E':
                    return DisplayCharactersEnum.HexE;
                case 'f':
                case 'F':
                    return DisplayCharactersEnum.HexF;
                default:
                    return DisplayCharactersEnum.None;
            }
        }

        public void ShiftIn(char c)
        {
            var pattern = GetBitPattern(c);
            if (!pattern)
            {
                throw new InvalidExpressionException("This is not a valid HEX character!");
            }
            else
            {
                for (var i = 0; i < 8; i++)
                {
                    sdiPin.PinWrite(pattern & (0x80 >> i) > 0);
                    _pulse(srclkPin);
                }
                _pulse(rclkPin);
            }
        }

        public ShiftRegisterService()
        {
            sdiPin.PinWrite(false);
            rclkPin.PinWrite(false);
            srclkPin.PinWrite(false);
            _pulse(rclkPin);
        }
    }
    
}