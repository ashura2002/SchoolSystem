using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands
{
    public record CreateUserCommand(string Username, string Email, string Password);
}
