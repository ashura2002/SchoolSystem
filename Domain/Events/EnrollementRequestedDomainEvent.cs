using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class EnrollementRequestedDomainEvent(Guid enrollmentId, Guid studentId, Guid classId) : IDomainEvent
    {
        public Guid EnrollmentId { get; private set; } = enrollmentId;
        public Guid StudentId { get; private set; } = studentId;
        public Guid ClassId { get; private set; } = classId;

    }
}
