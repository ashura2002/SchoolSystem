using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class LastNameVO
    {
        public string Value { get; }

        private LastNameVO(string value)
        {
            Value = value;
        }

        public static LastNameVO Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainBadRequestException("Last name cannot be empty.");
            value = value.Trim();
            if (value.Length < 3) throw new DomainBadRequestException("Last name must above 3 characters length.");
            return new LastNameVO(value);
        }
    }
}
