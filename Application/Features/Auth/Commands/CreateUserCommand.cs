using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands
{
    public record CreateUserCommand(string Username, string Email, string Password);
}
