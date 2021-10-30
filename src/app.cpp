// main.cpp
#include <console/application.h>

#include "../include/app.hpp"

bool beVerbose = false;

int main(int argc, char * argv[])
{
    Console::Application app(argc, argv);

    app.setApplicationName("RPi Test Suite");
    app.setApplicationUsage("./bin/rpi-test [command] [options]");
    app.setApplicationVersion("0.1.4");
    app.setAutoPrintHelp(true);

    app.setApplicationDescription("A more or less complete test suite for RaspberryPi's GPIO header");

    app.addGlobalOption("--Verbose", "Be extra verbose in output", "-V");
    app.addGlobalOption("--gui", "Run the TUI/GUI element of this app", "-g");

    auto appOptions = app.getAvailableGlobalOptions();
    beVerbose = appOptions->getOption("Verbose");

    app.addCommand(new TestShiftRegister);
    app.addCommand(new WritePin);

    return app.run();
}