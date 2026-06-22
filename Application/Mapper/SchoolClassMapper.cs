using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapper
{
    public class SchoolClassMapper
    {
        public static SchoolClassDTO ToDto(SchoolClass schoolClass)
        {
            return new SchoolClassDTO
            {
                Id = schoolClass.Id,
                Name = schoolClass.Name.Value,
                TeacherId = schoolClass.TeacherId,
                CreatedAt = schoolClass.CreatedAt,
                UpdatedAt = schoolClass.UpdatedAt
            };
        }

        public static IEnumerable<SchoolClassDTO> ToResponseList(IEnumerable<SchoolClass> schoolClasses)
        {
            return schoolClasses.Select(sc => new SchoolClassDTO
            {
                Id = sc.Id,
                Name = sc.Name.Value,
                TeacherId = sc.TeacherId,
                CreatedAt = sc.CreatedAt,
                UpdatedAt = sc.UpdatedAt
            });
        }

    }
}
