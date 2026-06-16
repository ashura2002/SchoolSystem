using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class UserUnauthorizedException : Exception
    {
        public UserUnauthorizedException(string message) : base(message) { }
    }
}
