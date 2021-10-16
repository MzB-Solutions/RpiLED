#include "../include/main.h"

#include <wiringPi.h>

/// <summary>
/// Take a pin from original state to opposite and back
/// </summary>
/// <param name="pin">physical pin id</param>
/// <param name="inverted">if true, we are dealing with a ACTIVE_LOW pin</param>
void pulse(const int pin, const bool inverted = false) {
	digitalWrite(pin, inverted);
	delay(3);
	digitalWrite(pin, !inverted);
	delay(3);
	digitalWrite(pin, inverted);
}

/// <summary>
/// ShiftIn a value of 8bits (char) width
/// </summary>
/// <param name="byte">any valid 8 bit sequence / int</param>
void si(unsigned char byte) {
	for (int i = 0; i <= 7; i++)
	{
		const bool val = (byte & (0x80 >> i)) > 0;
		digitalWrite(SDI, val);
		digitalWrite(SDI_LED, val);
		delay(4);
		pulse(SRCLK);
		pulse(SRCLK_LED);
	}
}

/// <summary>
/// Let's setup all our pins into their correct states.
/// </summary>
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

/// <summary>
/// This is the sequence of events that the 74HC595 ShiftRegister expects to fully reset it's output.
/// </summary>
void reset() {
	digitalWrite(RST, 0);
	pulse(RCLK);
	pulse(RCLK_LED);
	// this delay is required to satisfy the timing requirements of the 74HC595, ideally we only need 500 nano, but i digress
	delayMicroseconds(1);
	digitalWrite(RST, 1);
}


int main(void) {
	// This is the inetrnal iterator for our loop runs
	int _ii;
	// let's set up a pseudo-random rng source for std::shuffle
	std::random_device rd;
	std::mt19937 g(rd());
	// setup wiringPi to use physical pin notation
	if (wiringPiSetupPhys()==-1){
		printf("Can't access GPIO pins");
		return 1;
	}
	init();
	reset();
	// we just run up and down our SR pins from Q0-Q7
	for (int loops = 0; loops < MAXLOOPS; loops++)
	{
		for (_ii = 0; _ii < 8; _ii++) {
			si(led_out[_ii]);
			pulse(RCLK); pulse(RCLK_LED);
		}
		for (_ii = 0; _ii < 8; _ii++) {
			si(led_out[8 - _ii - 1]);
			pulse(RCLK); pulse(RCLK_LED);
		}
	}
	reset();
	delay(50);
	// Lets run all characters in their order on the 7 segment display
	cout << "Clean run .." << endl;
	for (_ii = 0; _ii <= 33; _ii++)
	{
		si(display_out[_ii]);
		cout << "iterator -> "  << _ii << " = " << display_out[_ii] << " <- value" << endl;
		pulse(RCLK); pulse(RCLK_LED);
		delay(20);
	}
	// Let's shuffle our characters and loop them MAXLOOPS/4 times
	cout << "Shuffled runs .." << endl;
	for (_ii = 0; _ii < MAXLOOPS/4; _ii++)
	{
		cout << " Run #" << i << endl;
		char byte[34] = {};
		copy_n(display_out, sizeof(display_out), byte);
		shuffle(byte, byte+34, g);
		for (_ii = 0; _ii <= 33; _ii++)
		{
			si(byte[_ii]);
			cout << "iterator -> "  << _ii << " = " << display_out[_ii] << " <- value" << endl;
			pulse(RCLK); pulse(RCLK_LED);
			delay(20);
		}
	}
	delay(50);
	reset();
	si(display_out[0]);
	pulse(RCLK, false); pulse(RCLK_LED, false);
	reset();
	return 0;
}