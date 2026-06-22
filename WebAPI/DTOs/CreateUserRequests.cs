using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class CreateUserRequests
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
