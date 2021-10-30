#include "test_shiftregister.hpp"
#include "../include/ShiftRegister.hpp"


/**
 * Retrieve the name of the command.
 *
 * @return std::string
 */
std::string TestShiftRegister::getName()
{
    return "test:shiftregister";
}

/**
 * Retrieve the description of the command.
 *
 * @return std::string
 */
std::string TestShiftRegister::getDescription()
{
    return "Test the ShiftRegister";
}

/**
 * Retrieve the command options.
 *
 * @return Types::AvailableOptions
 */
Types::AvailableOptions TestShiftRegister::getOptions()
{
    Types::AvailableOptions options;

    return options;
}

/**
 * Handle the command.
 *
 * @param InputInterface * input
 * @param OutputInterface * output
 * @return ExitCode
 */
ExitCode TestShiftRegister::handle(Interfaces::InputInterface * input, Interfaces::OutputInterface * output)
{
    if (input->wantsHelp()) {
        output->printCommandHelp(this);
        return ExitCode::NeedHelp;
    }

    // Implement something
    auto mySR = new shift_register();
    mySR->beVerbose = input->getOption("Verbose");
    mySR->run();


    return ExitCode::Ok;
}