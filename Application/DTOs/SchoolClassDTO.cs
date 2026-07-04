using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class SchoolClassDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? TeacherId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DayOfWeek Schedule { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
