using Application.Interfaces.CustomeMediatR.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands
{
    public record LoginCommand(string Username, string Password):ICommandWithResponse<string>;
}
