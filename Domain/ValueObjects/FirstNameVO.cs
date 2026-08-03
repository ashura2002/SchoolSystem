using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class FirstNameVO
    {
        public string Value { get; }

        private FirstNameVO(string value)
        {
            Value = value;
        }

        public static FirstNameVO Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new DomainBadRequestException("First name cannot be null");
            value = value.Trim();
            if (value.Length < 3) throw new DomainBadRequestException("First name must above 3 characters length.");
            return new FirstNameVO(value);
        }

    }
}
