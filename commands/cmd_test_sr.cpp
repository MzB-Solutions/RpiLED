#include "cmd_test_sr.hpp"


/**
 * Retrieve the name of the command.
 *
 * @return std::string
 */
std::string cmdTestShiftRegister::getName()
{
    return "cmdTestShiftRegister";
}

/**
 * Retrieve the description of the command.
 *
 * @return std::string
 */
std::string cmdTestShiftRegister::getDescription()
{
    return "Test the ShiftRegister";
}

/**
 * Retrieve the command options.
 *
 * @return Types::AvailableOptions
 */
Types::AvailableOptions cmdTestShiftRegister::getOptions()
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
ExitCode cmdTestShiftRegister::handle(Interfaces::InputInterface * input, Interfaces::OutputInterface * output)
{
    if (input->wantsHelp()) {
        output->printCommandHelp(this);
        return ExitCode::NeedHelp;
    }

	// Implement something

    return ExitCode::Ok;
}