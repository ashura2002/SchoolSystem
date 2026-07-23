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
            return new SchoolClassDTO(schoolClass.Id,
                schoolClass.Name.Value,
                schoolClass.TeacherId,
                schoolClass.StartTime,
                schoolClass.EndTime,
                schoolClass.Schedule,
                schoolClass.CreatedAt,
                schoolClass.UpdatedAt,
                schoolClass.StudentCapacity,
                schoolClass.CurrentStudents,
                schoolClass.RemainingSlots);

        }

        public static List<SchoolClassDTO> ToResponseList(List<SchoolClass> schoolClasses)
        {
            var result = schoolClasses.Select(sc => new SchoolClassDTO
            (
                sc.Id,
                sc.Name.Value,
                sc.TeacherId,
                sc.StartTime,
                sc.EndTime,
                sc.Schedule,
                sc.CreatedAt,
                sc.UpdatedAt,
                sc.StudentCapacity,
                sc.CurrentStudents,
                sc.RemainingSlots
            )).ToList();
            return result;
        }

    }
}
