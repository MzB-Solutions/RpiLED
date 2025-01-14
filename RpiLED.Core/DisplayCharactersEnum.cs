﻿namespace RpiLed.Core
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
        HexZero = 0x3F,
        HexOne = 0x06,
        HexTwo = 0x5B,
        HexThree = 0x4F,
        HexFour = 0x66,
        HexFive = 0x6D,
        HexSix = 0x7D,
        HexSeven = 0x07,
        HexEight = 0x7F,
        HexNine = 0x67,
        HexA = 0x77,
        HexB = 0x7C,
        HexC = 0x39,
        HexD = 0x5E,
        HexE = 0x79,
        HexF = 0x71
    }
}