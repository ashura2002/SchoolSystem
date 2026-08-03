using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class EmailVO
    {
        public string Value { get; }

        private EmailVO(string value)
        {
            Value = value;
        }

        public static EmailVO Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainBadRequestException("Email cannot be empty.");

            value = value.Trim().ToLower();

            if (!IsValidEmail(value)) throw new DomainBadRequestException("Invalid email format.");

            return new EmailVO(value);
        }


        private static bool IsValidEmail(string email)
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

    }
}
