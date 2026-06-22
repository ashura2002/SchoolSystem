using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class AssignTeacherRequest
    {

        [Required]
        public required Guid TeacherId{ get; set; }
    }
}
