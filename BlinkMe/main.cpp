#include <wiringPi.h>
#include <stdio.h>

#define	SDI_LED	13 // Indicator led for serial in on SR
#define	SRCLK_LED 11 // Indicator led for SHCP on SR
#define	RCLK_LED 15 // Indicator led for STCP on SR
#define SDI 16 //serial data input
#define RCLK 18 //memory clock input(STCP)
#define SRCLK 29 //shift register clock input(SHCP)
#define RST 22 // Reset pin (active_low)

unsigned char LedOut[8] = { 0x01,0x02,0x04,0x08,0x10,0x20,0x40,0x80 };

/// <summary>
/// All hexa values on a 7 segment display, bit reversed
/// </summary>
unsigned char DisplayOut[34] = {
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

void pulse(int pin, bool inverted = false) {
	digitalWrite(pin, inverted);
	delay(8);
	digitalWrite(pin, !inverted);
	delay(8);
	digitalWrite(pin, inverted);
}


void SI(unsigned char byte) {
	for (int i = 0; i <= 7; i++)
	{
		bool val = (byte & (0x80 >> i)) > 0;
		digitalWrite(SDI, val);
		digitalWrite(SDI_LED, val);
		delay(8);
		pulse(SRCLK);
		pulse(SRCLK_LED);
	}
}

void init() {
	pinMode(SDI, OUTPUT);
	pinMode(RCLK, OUTPUT);
	pinMode(SRCLK, OUTPUT);
	pinMode(SDI_LED, OUTPUT);
	pinMode(RCLK_LED, OUTPUT);
	pinMode(SRCLK_LED, OUTPUT);
	pinMode(RST, OUTPUT);
	digitalWrite(SDI, 0);
	digitalWrite(SDI_LED, 0);
	digitalWrite(RCLK, 0);
	digitalWrite(RCLK_LED, 0);
	digitalWrite(SRCLK, 0);
	digitalWrite(SRCLK_LED, 0);
	// set reset (MR) to active_low
	digitalWrite(RST, 1);
}

void reset() {
	digitalWrite(RST, 0);
	pulse(RCLK);
	pulse(RCLK_LED);
	// this delay is required to satisfy the timing requirements of the 74HC595, ideally we only need 500 nano, but i digress
	delayMicroseconds(1);
	digitalWrite(RST, 1);
}

int main(void) {
	int i;
	if (wiringPiSetupPhys()==-1){
		printf("Can't access GPIO pins");
		return 1;
	}
	init();
	reset();
	for (int loops = 0; loops < 2; loops++)
	{
		for (i = 0; i < 8; i++) {
			SI(LedOut[i]);
			pulse(RCLK); pulse(RCLK_LED);
			delay(20);
			printf("i = %d\n", i);
		}
		delay(50);
		for (i = 0; i < 8; i++) {
			SI(LedOut[8 - i - 1]);
			pulse(RCLK); pulse(RCLK_LED);
			delay(20);
		}
		delay(50);
	}
	//for (i = 0; i < 4; i++) {
	//	SI(0xff);
	//	pulse(RCLK); pulse(RCLK_LED);
	//	delay(100);
	//	SI(0x00);
	//	pulse(RCLK); pulse(RCLK_LED);
	//	delay(100);
	//}
	//delay(200);
	
	reset();
	for (i = 0; i <= 33; i++)
	{
		SI(DisplayOut[i]);
		pulse(RCLK); pulse(RCLK_LED);
		delay(100);
	}
	SI(DisplayOut[0]);
	pulse(RCLK); pulse(RCLK_LED);
	delay(200);
	reset();
	for (i = 33; i >= 0; i--)
	{
		SI(DisplayOut[i]);
		pulse(RCLK); pulse(RCLK_LED);
		delay(100);
	}
	SI(DisplayOut[0]);
	pulse(RCLK); pulse(RCLK_LED);
	reset();
	return 0;
}