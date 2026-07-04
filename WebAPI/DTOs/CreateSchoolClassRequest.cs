using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class CreateSchoolClassRequest
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required TimeOnly StartTime { get; set; }
        [Required]
        public required TimeOnly EndTime { get; set; }
        [Required]
        public required DayOfWeek Schedule { get; set; }

    }
}
