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
            (
                enrollment.Id,
                enrollment.ClassId,
                enrollment.StudentId,
                enrollment.Status,
                enrollment.CreatedAt,
                enrollment.UpdatedAt,
                enrollment.DeletedAt
            );
        }
    

        public static List<EnrollmentDTO> ToResponseList(IEnumerable<EnrollmentDTO> enrollments)
        {
            return enrollments.Select(e => new EnrollmentDTO
            (
                e.Id,
                e.ClassId,
                e.StudentId,
                e.Status,
                e.CreatedAt,
                e.UpdatedAt,
                e.DeletedAt
            )).ToList() ;
        }

    }
}
