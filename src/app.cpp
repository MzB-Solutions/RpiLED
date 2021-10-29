// main.cpp
#include <console/application.h>

#include "include/app.hpp"
#include "include/cmd_pin_write.h"


int main(int argc, char * argv[])
{
    Console::Application app(argc, argv);

    app.setApplicationName("RPi Test Suite");
    app.setApplicationUsage("./bin/rpi-test [command] [options]");
    app.setApplicationVersion("1.0.0");
    app.setAutoPrintHelp(true);

    app.setApplicationDescription("A more or less complete test suite for RaspberryPi's GPIO header");

    app.addGlobalOption("--test", "Testing the application", "-t");

    app.addCommand(new cmdPinWrite);

    return app.run();
}