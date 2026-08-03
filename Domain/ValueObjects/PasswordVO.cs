using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class PasswordVO
    {
        public string Value { get; }

        private PasswordVO(string value)
        {
            Value = value;
        }

        public static PasswordVO Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new DomainBadRequestException("Password cannot be empty.");
            value = value.Trim();
            if (value.Length < 5) throw new DomainBadRequestException("Invalid. Password must above 5 character.");
            return new PasswordVO(value);

        }
    }
}
