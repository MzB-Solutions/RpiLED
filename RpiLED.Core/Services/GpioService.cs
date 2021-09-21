using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Gpio;

namespace RpiLED.Core.Services
{

    public enum PinScheme
    {
        Physical = 1,
        Broadcom = 2,
        None = -1
    };

    public enum PinDirection
    {
        In,
        Out
    };

    public enum PinMode
    {
        Gpio = 1,
        Pwm = 2,
        None = -1
    };

    public class GpioService
    {
        public GpioController Gpio { get; private set; }

        private void SetScheme()
        {
            Gpio = new GpioController(PinNumberingScheme.Board);
        }

        public GpioService()
        {
            SetScheme();
        }
    }
}
