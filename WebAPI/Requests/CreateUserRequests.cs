using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Requests
{
    public class CreateUserRequests
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(3)]
        public string Password { get; set; } = null!;
    }
}
