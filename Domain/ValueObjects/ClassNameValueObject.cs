using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class ClassNameValueObject
    {
        public string Value { get; }

        private ClassNameValueObject(string value)
        {
            Value = value;
        }

        public static ClassNameValueObject Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainBadRequestException("Class name cannot be empty");
            value = value.Trim().ToUpperInvariant();

            if (value.Length < 3) throw new DomainBadRequestException("Class name must above 3 characters length.");

            return new ClassNameValueObject(value);
        }
    }
}
