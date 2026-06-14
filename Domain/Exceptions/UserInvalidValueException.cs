using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class UserInvalidValueException : Exception
    {
        public UserInvalidValueException(string message) : base(message) { }
    }
}
