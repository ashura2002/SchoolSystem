using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class UsernameValueObject
    {
        // field
        public string Value { get; }

        // constructor
        private UsernameValueObject(string value)
        {
            Value = value;
        }

        // method
        public static UsernameValueObject Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainBadRequestException("Username cannot be empty.");
            value = value.Trim();

            if (value.Length < 3)
                throw new DomainBadRequestException("Username must be at least 3 characters long.");
            return new UsernameValueObject(value);
        }
    }
}
