using System;
using System.Collections.Generic;
using System.Linq;
using RpiLED.Core.Models;

namespace RpiLed.Core.Services
{
    public class ShiftRegisterService
    {
        public List<DisplayCharactersEnum> Characters =
        Enum.GetValues(typeof(DisplayCharactersEnum))
        .Cast<DisplayCharactersEnum>()
        .ToList();
    }


}