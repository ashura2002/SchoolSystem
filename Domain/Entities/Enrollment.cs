using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Enrollment
    {
        public Guid Id { get; private set; }

        public Guid StudentId { get; private set; }
        public Guid ClassId { get; private set; }
        public EnrollmentStatus Status { get; private set; }

        public DateTime EnrolledAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Enrollment(Guid studentId, Guid classId)
        {
            Id = Guid.NewGuid();

            StudentId = studentId;
            ClassId = classId;

            Status = EnrollmentStatus.Pending;  
            EnrolledAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Approve()
        {
            Status = EnrollmentStatus.Approved;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Reject()
        {
            Status = EnrollmentStatus.Rejected;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
