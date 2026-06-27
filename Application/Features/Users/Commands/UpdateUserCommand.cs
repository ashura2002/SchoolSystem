using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands
{
    public record UpdateUserCommand(Guid UserId, string Username, string Password);
}
