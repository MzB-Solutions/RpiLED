#include "main.h"

#include <wiringPi.h>
#include <stdio.h>



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