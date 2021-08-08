using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Exceptions
{
    public class AgeLimitException : Exception
    {
        public AgeLimitException(string message) : base(message)
        {
        }
    }
}
