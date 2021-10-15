namespace RpiLed.Core
{
    public enum DisplayCharactersEnum : byte
    {
        //None = 0b00000000,
        //HexZero = 0b11111100,
        //HexOne = 0b01100000,
        //HexTwo = 0b11011010,
        //HexThree = 0b11110010,
        //HexFour = 0b01100110,
        //HexFive = 0b10110110,
        //HexSix = 0b10111110,
        //HexSeven = 0b11100000,
        //HexEight = 0b11111110,
        //HexNine = 0b11100110,
        //HexA = 0b11101110,
        //HexB = 0b00111110,
        //HexC = 0b10011100,
        //HexD = 0b01111010,
        //HexE = 0b10011110,
        //HexF = 0b10001110
        // == ABOVE AND BELOW ARE IDENTICAL values, base2 vs base16 (ie: binary vs hex)
        None = 0x00,
        HexZero = 0xFC,
        HexOne = 0x60,
        HexTwo = 0xDA,
        HexThree = 0xF2,
        HexFour = 0x66,
        HexFive = 0xB6,
        HexSix = 0xBE,
        HexSeven = 0xE0,
        HexEight = 0xFE,
        HexNine = 0xE6,
        HexA = 0xEE,
        HexB = 0x3E,
        HexC = 0x9C,
        HexD = 0x7A,
        HexE = 0x9E,
        HexF = 0x8E
    }
}