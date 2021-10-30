#include "../include/ShiftRegister.hpp"

#include <wiringPi.h>

/// <summary>
/// Take a pin from original state to opposite and back
/// </summary>
/// <param name="pin">physical pin id</param>
/// <param name="inverted">if true, we are dealing with a ACTIVE_LOW pin</param>
void shift_register::pulse(const int pin, const bool inverted = false) {
	digitalWrite(pin, inverted);
	delay(2);
	digitalWrite(pin, !inverted);
	delay(2);
	digitalWrite(pin, inverted);
}

/// <summary>
/// ShiftIn a value of 8bits (char) width
/// </summary>
/// <param name="byte">any valid 8 bit sequence / int</param>
void shift_register::si(unsigned char byte) {
	for (int i = 0; i <= 7; i++)
	{
		digitalWrite(DS, (byte & (0x80 >> i)) > 0);
		//delay(1);
		pulse(SH_CP);
	}
	pulse(ST_CP);
	delay(20);
}

/// <summary>
/// Let's setup all our pins into their correct states.
/// </summary>
void shift_register::init() {
	pinMode(DS, OUTPUT);
	pinMode(ST_CP, OUTPUT);
	pinMode(SH_CP, OUTPUT);
	pinMode(MR, OUTPUT);
	digitalWrite(DS, 0);
	digitalWrite(ST_CP, 0);
	digitalWrite(SH_CP, 0);
	// set reset (MR) to active_low
	digitalWrite(MR, 1);
}

/// <summary>
/// This is the sequence of events that the 74HC595 ShiftRegister expects to fully reset it's output.
/// </summary>
void shift_register::reset() {
	digitalWrite(MR, 0);
	pulse(ST_CP);
	// this delay is required to satisfy the timing requirements of the 74HC595, ideally we only need 500 nano, but i digress
	delayMicroseconds(1);
	digitalWrite(MR, 1);
}


int shift_register::run(void) {
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
			const auto val = led_out[_ii];
			si(val);
			cout << "step: "  << _ii << " = " << bitset<8>(val) << endl;
		}
		for (_ii = 0; _ii < 8; _ii++) {
			const auto val = led_out[8 - _ii - 1];
			si(val);
			cout << "step: "  << _ii << " = " << bitset<8>(val) << endl;
		}
	}
	reset();
	// Lets run all characters in their order on the 7 segment display
	cout << "Clean run .." << endl;
	for (_ii = 0; _ii <= 33; _ii++)
	{
		const auto val = display_out[_ii];
		si(val);
		cout << "iterator -> "  << _ii << " = " << bitset<8>(val) << " <- value" << endl;
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
			const auto val = byte[_ii];
			si(val);
			cout << "iterator -> "  << _ii << " = " << bitset<8>(val) << " <- value" << endl;
		}
	}
	reset();
	si(display_out[0]);
	reset();
	return 0;
}