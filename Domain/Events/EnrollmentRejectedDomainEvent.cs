using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class EnrollmentRejectedDomainEvent(Guid studentId, Guid classId) : IDomainEvent
    {
        public Guid StudentId { get; private set; } = studentId;
        public Guid ClassId { get; private set; } = classId;

    }
}
