using Domain.Enums;
using Domain.Events;
using Domain.Exceptions;
using System;
using System.Diagnostics;
using System.Text;

namespace Domain.Entities
{
    public class Enrollment : AggregateRoot
    {
        public Guid StudentId { get; private set; }
        public Guid ClassId { get; private set; }
        public EnrollmentStatus Status { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        private Enrollment(Guid studentId, Guid classId)
        {
            StudentId = studentId;
            ClassId = classId;
            Status = EnrollmentStatus.Pending;
        }

        // factory method for requesting enrollment
        public static Enrollment Request(Guid studentId, Guid classId)
        {
            Enrollment enrollment = new(studentId, classId);
            enrollment.RaiseEvent(new EnrollmentRequestedDomainEvent(studentId, classId));
            return enrollment;
        }

        public void Approve()
        {
            if (Status != EnrollmentStatus.Pending)
                throw new DomainBadRequestException("Only pending enrollments can be approved");
            RaiseEvent(new EnrollmentApprovedDomainEvent(StudentId, ClassId));
            Status = EnrollmentStatus.Approved;
            Touch();
        }

        public void Reject()
        {
            if (Status != EnrollmentStatus.Pending)
                throw new DomainBadRequestException("Only pending enrollments can be rejected");
            RaiseEvent(new EnrollmentRejectedDomainEvent(StudentId, ClassId));
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
