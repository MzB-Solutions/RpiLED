// main.cpp
#include <console/application.h>

#include "../include/app.hpp"


int main(int argc, char * argv[])
{
    Console::Application app(argc, argv);

    app.setApplicationName("RPi Test Suite");
    app.setApplicationUsage("./bin/rpi-test [command] [options]");
    app.setApplicationVersion("0.1.4");
    app.setAutoPrintHelp(true);

    app.setApplicationDescription("A more or less complete test suite for RaspberryPi's GPIO header");

    app.addGlobalOption("--verbose", "Be extra verbose in output", "-v");
    app.addGlobalOption("--gui", "Run the TUI/GUI element of this app", "-g");

    app.addCommand(new TestShiftRegister);
    app.addCommand(new WritePin);

    return app.run();
}