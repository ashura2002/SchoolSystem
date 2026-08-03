
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class CreateProfileRequest
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Address { get; set; }
        [Required]
        public required DateOnly DateOfBirth { get; set; }
    }
}
