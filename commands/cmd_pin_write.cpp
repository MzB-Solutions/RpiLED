#include "cmd_pin_write.hpp"


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
    return "Write a <value> to <pin>";
}

/**
 * Retrieve the command options.
 *
 * @return Types::AvailableOptions
 */
Types::AvailableOptions cmdPinWrite::getOptions()
{
    Types::AvailableOptions options;

    options["-p"] = std::pair<std::string, std::string>("--pin", "specify the physical pin");
    options["-v"] = std:pair<std::string, std::string>("--value", "specify the value to send to the pin");

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

    if (input->getOption("pin").empty() && input->getOption("value").empty())
    {
        output->warning("missing options ..");
        output->printCommandHelp(this);
        return ExitCode::NeedHelp;
    }

    auto pin = input->getOption("pin");
    auto value = input->getOption("value");
    output->info("We would be writing %s to pin %s", pin.c_str(), value.c_str());
	// Implement something

    return ExitCode::Ok;
}