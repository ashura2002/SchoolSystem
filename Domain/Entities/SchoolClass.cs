using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SchoolClass
    {
        public Guid Id { get; private set; }
        public ClassNameValueObject Name { get; private set; }
        public Guid? TeacherId { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }


        public SchoolClass(ClassNameValueObject name)
        {
            Id = Guid.NewGuid();
            Name = name;
            TeacherId = null;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateClassName(ClassNameValueObject newClassName)
        {
            if (Name == newClassName) return;

            Name = newClassName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignTeacher(Guid teacherId)
        {
            if (TeacherId != null)
                throw new DomainBadRequestException("This class already has an assigned teacher.");

            if (teacherId == Guid.Empty)
                throw new DomainBadRequestException("Teacher Id cannot be empty.");

            TeacherId = teacherId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveTeacher()
        {
            if (!HasTeacher) throw new DomainBadRequestException("No teacher is assigned to this class.");
            TeacherId = null;
            UpdatedAt = DateTime.UtcNow;
        }

        public void EnsureCanBeDeleted()
        {
            if (HasTeacher)
                throw new DomainBadRequestException("Remove the assigned teacher before deleting the class.");
        }

        public bool HasTeacher => TeacherId != null;

    }
}
