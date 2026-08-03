using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record UserDTO(
        Guid Id,
        string Username,
        string Email,
        Role Role,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        DateTime? DeletedAt
   );
}
