#include "../include/cmd_pin_write.hpp"


/**
 * Retrieve the name of the command.
 *
 * @return std::string
 */
std::string cmdPinWrite::getName()
{
    return "cmd:pin:write";
}

/**
 * Retrieve the description of the command.
 *
 * @return std::string
 */
std::string cmdPinWrite::getDescription()
{
    return "Pin write command";
}

/**
 * Retrieve the command options.
 *
 * @return Types::AvailableOptions
 */
Types::AvailableOptions cmdPinWrite::getOptions()
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
ExitCode cmdPinWrite::handle(Interfaces::InputInterface * input, Interfaces::OutputInterface * output)
{
    if (input->wantsHelp()) {
        output->printCommandHelp(this);
        return ExitCode::NeedHelp;
    }

	// Implement something

    return ExitCode::Ok;
}