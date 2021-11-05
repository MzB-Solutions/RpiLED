using System;
using System.Threading;
using Iot.Device.Multiplexing;
using RpiLed.Core;

namespace RpiLED.Core.Services
{
    public class ShiftRegisterService
    {
        #region Public Constructors

        public ShiftRegisterService()
        {
            var pinMapping = new ShiftRegisterPinMapping(Sdi, Srclk, Rclk);
            _sr = new ShiftRegister(pinMapping, 8, new GpioService().Gpio);
        }

        #endregion Public Constructors

        #region Private Destructors

        ~ShiftRegisterService()
        {
            _sr.Dispose();
        }

        #endregion Private Destructors

        #region Private Fields

        // Storage Register clock (ST_CP)
        private const int Rclk = 18;

        // Reset (MR) Pin
        private const int Rst = 22;

        // Serial Data In  (DS) Pin
        private const int Sdi = 16;

        // Shift register clock (SH_CP)
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

        private readonly ShiftRegister _sr;

        #endregion Private Fields

        #region Private Methods

        private static byte GetBitPattern(char character)
        {
            return character switch
            {
                '0' => (byte) DisplayCharactersEnum.HexZero,
                '1' => (byte) DisplayCharactersEnum.HexOne,
                '2' => (byte) DisplayCharactersEnum.HexTwo,
                '3' => (byte) DisplayCharactersEnum.HexThree,
                '4' => (byte) DisplayCharactersEnum.HexFour,
                '5' => (byte) DisplayCharactersEnum.HexFive,
                '6' => (byte) DisplayCharactersEnum.HexSix,
                '7' => (byte) DisplayCharactersEnum.HexSeven,
                '8' => (byte) DisplayCharactersEnum.HexEight,
                '9' => (byte) DisplayCharactersEnum.HexNine,
                'A' => (byte) DisplayCharactersEnum.HexA,
                'B' => (byte) DisplayCharactersEnum.HexB,
                'C' => (byte) DisplayCharactersEnum.HexC,
                'D' => (byte) DisplayCharactersEnum.HexD,
                'E' => (byte) DisplayCharactersEnum.HexE,
                'F' => (byte) DisplayCharactersEnum.HexF,
                'a' => (byte) DisplayCharactersEnum.HexA,
                'b' => (byte) DisplayCharactersEnum.HexB,
                'c' => (byte) DisplayCharactersEnum.HexC,
                'd' => (byte) DisplayCharactersEnum.HexD,
                'e' => (byte) DisplayCharactersEnum.HexE,
                'f' => (byte) DisplayCharactersEnum.HexF,
                _ => (byte) DisplayCharactersEnum.None
            };
        }

        private void ResetSr()
        {
            _sr.ShiftClear();
        }

        private void Si(byte ch)
        {
            _sr.ShiftByte(ch);
        }

        #endregion Private Methods

        #region Public Methods

        public void RunTest()
        {
            ResetSr();
            Console.WriteLine(@"Test1");
            for (var i = 0; i < 8; i++)
            {
                Si(ShiftOutput[i]);
                Thread.Sleep(200);
            }

            Console.WriteLine(@"Reset");
            Thread.Sleep(500);
            for (var i = 0; i < 4; i++)
            {
                Si(0xff);
                Thread.Sleep(200);
                Si(0x00);
                Thread.Sleep(200);
            }

            Console.WriteLine(@"Test2");
            Thread.Sleep(500);
            for (var i = 0; i < 8; i++)
            {
                Si(ShiftOutput[8 - i - 1]);
                Thread.Sleep(200);
            }

            Console.WriteLine(@"Reset");
            Thread.Sleep(500);
            for (var i = 0; i < 4; i++)
            {
                Si(0xff);
                Thread.Sleep(200);
                Si(0x00);
                Thread.Sleep(200);
            }
            for (var i=0; i<=15; i++) {
                //do something here
            }
            ResetSr();
            Console.WriteLine(@"testing HexA");
            Si((byte) DisplayCharactersEnum.HexA);
            Thread.Sleep(1000);
            ResetSr();
            Console.WriteLine(@"testing Hex0");
            Si((byte) DisplayCharactersEnum.HexZero);
            Thread.Sleep(1000);
            ResetSr();
            Console.WriteLine(@"testing Hex8");
            Si((byte) DisplayCharactersEnum.HexEight);
            Thread.Sleep(1000);
            ResetSr();
        }

        public void ShiftIn(char c)
        {
            ResetSr();
            var bitPattern = GetBitPattern(c);
            Console.WriteLine(@"This is the byte sequence we're gonna send : " + bitPattern);
            _sr.ShiftByte(bitPattern);
            Thread.Sleep(100);
        }

        #endregion Public Methods
    }
}
