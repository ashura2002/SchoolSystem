using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record CreateClassDTO(string Name);
    public record AssignTeacherDTO(Guid Id);
    public record UpdateClassNameDTO(string Name);
}
