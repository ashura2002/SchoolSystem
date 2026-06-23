using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class UpdateClassNameRequest
    {
        [Required]
        public required string Name { get; set; }
    }
}
