using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapper
{
    public class TeacherClassMapper
    {

        public static TeacherClassDetailDTO ToDto(
            SchoolClass schoolClass,
            List<User> users
            )
        {
            return new TeacherClassDetailDTO(schoolClass.Id, schoolClass.Name.Value,
                users.Select(s => new Students(s.Id, s.Username.Value))
                .ToList()
                );
        }

    }
}
