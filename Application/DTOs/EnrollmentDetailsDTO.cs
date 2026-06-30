using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record EnrollmentDetailsDTO(Guid EnrollmentId, string ClassName, EnrollmentStatus EnrollmentStatus);

    public record EnrollmentResponseDTO(Guid EnrollmenId, EnrollmentStatus EnrollmentStatus, string ClassName,
        DateTime CreatedAt, DateTime UpdatedAt, DateTime? DeletedAt);

    public record PendingEnrollmentResponseDTO(Guid EnrollmenId, EnrollmentStatus EnrollmentStatus, string StudentName,
        string ClassName, DateTime CreatedAt, DateTime UpdatedAt, DateTime? DeletedAt);
}
