using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class EnrollmentDTO
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
        public EnrollmentStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
