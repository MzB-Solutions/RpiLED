#include "../include/main.h"

#include <wiringPi.h>

/// <summary>
/// Take a pin from original state to opposite and back
/// </summary>
/// <param name="pin">physical pin id</param>
/// <param name="inverted">if true, we are dealing with a ACTIVE_LOW pin</param>
void pulse(const int pin, const bool inverted = false) {
	digitalWrite(pin, inverted);
	//delay(1);
	delayMicroseconds(500);
	digitalWrite(pin, !inverted);
	delayMicroseconds(500);
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
		delay(5);
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
	// This is the internal iterator for our ShiftIn runs
	int _ii;
	// This is the internal loop iterator tied into MaxLoops
	int _li;
	// Maximum amount of single loop runs (this is fixed at COMPILE time)
	// as opposed to a const which might get evaluated at runtime and NOT at compile time
	constexpr int max_loops = 4;
	// Quarter of amount loops to loop through
	constexpr int quarter_loops = max_loops/4;
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
	for (_li = 0; _li < max_loops; _li++)
	{
		cout << "run: "  << _li << endl;
		for (_ii = 0; _ii < 8; _ii++) {
			si(led_out[_ii]);
			cout << "step: "  << _ii << " = " << bitset<8>(led_out[_ii]) << endl;
			pulse(RCLK); pulse(RCLK_LED);
		}
		for (_ii = 0; _ii < 8; _ii++) {
			si(led_out[8 - _ii - 1]);
			cout << "step: "  << _ii << " = " << bitset<8>(led_out[8 - _ii - 1]) << endl;
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
		cout << "iterator -> "  << _ii << " = " << bitset<8>(display_out[_ii]) << " <- value" << endl;
		pulse(RCLK); pulse(RCLK_LED);
		delay(20);
	}
	cout << "Doing " << quarter_loops <<" Shuffled loops .." << endl;
	// Let's shuffle our characters and loop them QuarterLoops times
	for (_li = 0; _li < quarter_loops; _li++)
	{
		cout << " Loop #" << _li+1 << endl;
		char byte[34] = {};
		copy_n(display_out, sizeof(display_out), byte);
		shuffle(byte, byte+34, g);
		for (_ii = 0; _ii <= 33; _ii++)
		{
			si(byte[_ii]);
			cout << "iterator -> "  << _ii << " = " << bitset<8>(display_out[_ii]) << " <- value" << endl;
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