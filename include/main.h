#pragma once
#include <algorithm>
#include <iostream>
// ReSharper disable once CppUnusedIncludeDirective
#include <random>

// Indicator led for serial in on SR
#define	SDI_LED	13
// Indicator led for SHCP on SR
#define	SRCLK_LED 11
// Indicator led for STCP on SR
#define	RCLK_LED 15
//serial data input
#define SDI 16
//memory clock input(STCP)
#define RCLK 18
//shift register clock input(SHCP)
#define SRCLK 29
// Reset pin (active_low)
#define RST 22

using std::cout;
using std::endl;
using std::shuffle;
using std::copy_n;

unsigned char led_out[8] = { 0x01,0x02,0x04,0x08,0x10,0x20,0x40,0x80 };

/// <summary>
/// All hexa values on a 7 segment display, bit reversed
/// </summary>
unsigned char display_out[34] = {
	0b00000000, // All off
	0b10000000, // the dot only
	0b00111111, // 0
	0b10111111, // 0 with dot
	0b00000110, // 1
	0b10000110, // 1 with dot
	0b01011011, // 2
	0b11011011, // 2 with dot
	0b01001111, // 3
	0b11001111, // 3 with dot
	0b01100110, // 4
	0b11100110, // 4 with dot
	0b01101101, // 5
	0b11101101, // 5 with dot
	0b01111101, // 6
	0b11111101, // 6 with dot
	0b00000111, // 7
	0b10000111, // 7 with dot
	0b01111111, // 8
	0b11111111, // 8 with dot
	0b01100111, // 9
	0b11100111, // 9 with dot
	0b01110111, // A
	0b11110111, // A with dot
	0b01111100, // B
	0b11111100, // B with dot
	0b00111001, // C
	0b10111001, // C with dot
	0b01011110, // D
	0b11011110, // D with dot
	0b01111001, // E
	0b11111001, // E with dot
	0b01110001, // F
	0b11110001  // F with dot
};