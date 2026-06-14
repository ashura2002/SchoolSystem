using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class EmailValueObject
    {
        public string Value { get; }

        private EmailValueObject(string value)
        {
            Value = value;
        }

        public static EmailValueObject Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new UserInvalidValueException("Email cannot be empty.");

            value = value.Trim().ToLower();

            if (!IsValidEmail(value)) throw new UserInvalidValueException("Invalid email format.");

            return new EmailValueObject(value);
        }


        private static bool IsValidEmail(string email)
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

    }
}
