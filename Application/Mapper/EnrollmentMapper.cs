using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapper
{
    public class EnrollmentMapper
    {

        public static EnrollmentDTO ToDto(Enrollment enrollment)
        {
            return new EnrollmentDTO
            {
                Id = enrollment.Id,
                ClassId = enrollment.ClassId,
                StudentId = enrollment.StudentId,
                Status = enrollment.Status,
                CreatedAt = enrollment.CreatedAt
            };
        }
    

        public static IEnumerable<EnrollmentDTO> ToResponseList(IEnumerable<Enrollment> enrollments)
        {
            return enrollments.Select(e => new EnrollmentDTO
            {
                Id = e.Id,
                ClassId = e.ClassId,
                StudentId = e.StudentId,
                Status = e.Status,
                CreatedAt = e.CreatedAt
            });
        }

    }
}
