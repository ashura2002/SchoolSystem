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
                StartTime = schoolClass.StartTime,
                EndTime = schoolClass.EndTime,
                Schedule = schoolClass.Schedule,
                CreatedAt = schoolClass.CreatedAt,
                UpdatedAt = schoolClass.UpdatedAt
            };
        }

        public static List<SchoolClassDTO> ToResponseList(List<SchoolClass> schoolClasses)
        {
            var result = schoolClasses.Select(sc => new SchoolClassDTO
            {
                Id = sc.Id,
                Name = sc.Name.Value,
                TeacherId = sc.TeacherId,
                StartTime = sc.StartTime,
                EndTime = sc.EndTime,
                Schedule = sc.Schedule,
                CreatedAt = sc.CreatedAt,
                UpdatedAt = sc.UpdatedAt
            }).ToList();
            return result;
        }

    }
}
