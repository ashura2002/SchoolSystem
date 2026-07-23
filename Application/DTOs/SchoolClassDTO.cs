using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record SchoolClassDTO(Guid Id, 
        string Name, 
        Guid? TeacherId, 
        TimeOnly StartTime, 
        TimeOnly EndTime, 
        DayOfWeek Schedule,
        DateTime CreatedAt, 
        DateTime UpdatedAt, 
        int StudentCapacity, 
        int CurrentStudents, 
        int RemainingSlots);
}
