using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{

    public record Students(Guid StudentId, string Username);

    public record TeacherClassDetailDTO(
        Guid Id,
        string ClassName,
        TimeOnly StartTime,
        TimeOnly EndTime,
        DayOfWeek Schedule,
        List<Students> Students
        );
}
