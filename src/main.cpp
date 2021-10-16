#include "../include/main.h"

#include <wiringPi.h>

void pulse(int pin, bool inverted = false) {
	digitalWrite(pin, inverted);
	delay(2);
	digitalWrite(pin, !inverted);
	delay(2);
	digitalWrite(pin, inverted);
}

void SI(unsigned char byte) {
	for (int i = 0; i <= 7; i++)
	{
		bool val = (byte & (0x80 >> i)) > 0;
		digitalWrite(SDI, val);
		digitalWrite(SDI_LED, val);
		delay(4);
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
	std::random_device rd;
	std::mt19937 g(rd());

	if (wiringPiSetupPhys()==-1){
		printf("Can't access GPIO pins");
		return 1;
	}
	init();
	reset();
	for (int loops = 0; loops < MAXLOOPS; loops++)
	{
		for (i = 0; i < 8; i++) {
			SI(LedOut[i]);
			pulse(RCLK); pulse(RCLK_LED);
		}
		for (i = 0; i < 8; i++) {
			SI(LedOut[8 - i - 1]);
			pulse(RCLK); pulse(RCLK_LED);
		}
	}
	reset();
	delay(50);
	printf("Clean run");
	for (i = 0; i <= 33; i++)
	{
		SI(DisplayOut[i]);
		printf("iterator -> %d", i);printf(" = %d <- value\n", DisplayOut[i]);
		pulse(RCLK); pulse(RCLK_LED);
		delay(20);
	}
	printf("Shuffled runs");
	for (i = 0; i < MAXLOOPS/4; i++)
	{
		char byte[34] = {};
		copy(DisplayOut,DisplayOut+34,byte);
		shuffle(byte, byte+34, g);
		for (i = 0; i <= 33; i++)
		{
			SI(byte[i]);
			printf("iterator -> %d", i);printf(" = %d <- value\n", byte[i]);
			pulse(RCLK); pulse(RCLK_LED);
			delay(20);
		}
	}
	delay(50);
	reset();
	SI(DisplayOut[0]);
	pulse(RCLK); pulse(RCLK_LED);
	reset();
	return 0;
}