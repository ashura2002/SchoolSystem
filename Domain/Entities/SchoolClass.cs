using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SchoolClass : BaseEntity
    {
        public ClassNameValueObject Name { get; private set; }
        public Guid? TeacherId { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
        public DayOfWeek Schedule { get; private set; }

        public int StudentCapacity { get; private set; }
        public int CurrentStudents { get; private set; }
        public int RemainingSlots => StudentCapacity - CurrentStudents;

        private SchoolClass(ClassNameValueObject name, TimeOnly startTime, TimeOnly endTime, DayOfWeek schedule,
            int studentCapacity)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            Schedule = schedule;
            TeacherId = null;
            StudentCapacity = studentCapacity;
            CurrentStudents = 0;
        }


        // factory method 
        public static SchoolClass Create(ClassNameValueObject name, TimeOnly startTime, TimeOnly endTime, DayOfWeek schedule,
            int studentCapacity = 25)
        {
            if (endTime <= startTime)
                throw new DomainBadRequestException("End time must be after the start time.");

            if (studentCapacity <= 0) throw new DomainBadRequestException("Capacity must be greater than zero.");

            SchoolClass schoolClass = new(name, startTime, endTime, schedule, studentCapacity);

            return schoolClass;
        }

        public void UpdateClassName(ClassNameValueObject newClassName)
        {
            if (Name == newClassName) return;

            Name = newClassName;
            Touch();
        }

        public void UpdateSchedule(DayOfWeek schedule, TimeOnly startTime, TimeOnly endTime)
        {
            if (endTime <= startTime)
                throw new DomainBadRequestException(
                    "End time must be after the start time.");

            Schedule = schedule;
            StartTime = startTime;
            EndTime = endTime;
            Touch();
        }

        public void AssignTeacher(Guid teacherId)
        {
            if (TeacherId != null)
                throw new DomainBadRequestException("This class already has an assigned teacher.");

            if (teacherId == Guid.Empty)
                throw new DomainBadRequestException("Teacher Id cannot be empty.");

            TeacherId = teacherId;
            Touch();
        }

        public void RemoveTeacher()
        {
            if (!HasTeacher) throw new DomainBadRequestException("No teacher is assigned to this class.");
            TeacherId = null;
            Touch();
        }

        public void EnsureCanBeDeleted()
        {
            if (HasTeacher)
                throw new DomainBadRequestException("Remove the assigned teacher before deleting the class.");
        }

        public bool HasTeacher => TeacherId != null;

        // busines rule for students capacity
        public void EnrollStudent()
        {
            if (CurrentStudents >= StudentCapacity)
                throw new DomainBadRequestException("This class has reached its maximum student capacity.");

            CurrentStudents++;
            Touch();
        }

        public void RemoveStudent()
        {
            if (CurrentStudents <= 0) 
                throw new DomainBadRequestException("There are no students enrolled in this class.");

            CurrentStudents--;
            Touch();
        }
    }
}
