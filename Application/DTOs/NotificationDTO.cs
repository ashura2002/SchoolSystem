using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record NotificationDTO(Guid Id, Guid UserId, string Content, bool IsRead, DateTime CreatedAt, DateTime UpdatedAt);
 
}
