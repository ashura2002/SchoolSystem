using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class CreateSchoolClassRequest
    {
        [Required]
        public required string Name { get; set; }
    }
}
