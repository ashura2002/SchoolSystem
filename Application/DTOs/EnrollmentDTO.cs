using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record EnrollmentDTO(
        Guid Id,
        Guid StudentId,
        Guid ClassId,
        EnrollmentStatus Status,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        DateTime? DeletedAt);
}
