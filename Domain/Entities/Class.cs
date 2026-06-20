using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Class
    {
        public Guid Id { get; private set; }
        public ClassNameValueObject Name { get; private set; }
        public Guid TeacherId { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Class(ClassNameValueObject name, Guid teacherId)
        {
            Id = Guid.NewGuid();
            Name = name;
            TeacherId = teacherId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateClassName(ClassNameValueObject newClassName)
        {
            Name = newClassName;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
