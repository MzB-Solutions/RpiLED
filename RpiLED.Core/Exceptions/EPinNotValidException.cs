using System;

namespace RpiLed.Core.Exceptions
{
    internal class EPinNotValidException : Exception
    {
        #region Public Constructors

        public EPinNotValidException(string message) : base(Properties.ExceptionMessages.PinNotValidMsg + ":\n" + message)
        {
        }

        #endregion Public Constructors
    }
}