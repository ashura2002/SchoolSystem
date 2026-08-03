using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record UserWithProfileDetailDTO(
        Guid ProfileId,
        Guid UserId,
        string Username,
        string Email,
        string FirstName,
        string LastName,
        string Address,
        DateOnly DateOfBirth,
        string? ProfilePictureUrl,
        string?ProfilePicturePublicId);
}
