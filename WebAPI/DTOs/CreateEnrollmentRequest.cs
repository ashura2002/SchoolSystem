using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class CreateEnrollmentRequest
    {

        [Required]
        public required Guid ClassId { get; set; }
    }
}
