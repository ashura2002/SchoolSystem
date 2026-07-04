using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Enrollment:BaseEntity
    {
        public Guid StudentId { get; private set; }
        public Guid ClassId { get; private set; }
        public EnrollmentStatus Status { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public Enrollment(Guid studentId, Guid classId)
        {
            StudentId = studentId;
            ClassId = classId;
            Status = EnrollmentStatus.Pending;
        }

        public void Approve()
        {
            if (Status != EnrollmentStatus.Pending)
                throw new DomainBadRequestException("Only pending enrollments can be approved");

            Status = EnrollmentStatus.Approved;
            Touch();
        }

        public void Reject()
        {
            if (Status != EnrollmentStatus.Pending)
                throw new DomainBadRequestException("Only pending enrollments can be rejected");

            Status = EnrollmentStatus.Rejected;
            Touch();
        }

        public void Cancel()
        {
            if (Status != EnrollmentStatus.Pending)
                throw new DomainBadRequestException("Only pending enrollments can be cancelled");

            DeletedAt = DateTime.UtcNow;
            Touch();
        }

        public void Drop()
        {
            if (Status != EnrollmentStatus.Approved)
                throw new DomainBadRequestException("Only approved enrollments can be dropped");

            DeletedAt = DateTime.UtcNow;
            Touch();
        }
    }
}
