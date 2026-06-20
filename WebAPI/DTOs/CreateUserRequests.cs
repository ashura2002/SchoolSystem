using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class CreateUserRequests
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(3)]
        public required string Password { get; set; }
    }
}
