using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class PasswordValueObject
    {
        public string Value { get; }

        private PasswordValueObject(string value)
        {
            Value = value;
        }

        public static PasswordValueObject Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new BadRequestException("Password cannot be empty.");
            value = value.Trim();
            if (value.Length < 5) throw new BadRequestException("Invalid. Password must above 5 character.");
            return new PasswordValueObject(value);

        }
    }
}
