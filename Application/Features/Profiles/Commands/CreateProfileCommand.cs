using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Profiles.Commands
{
    public record CreateProfileCommand(
        string FirstName, 
        string LastName, 
        string Address,
        DateOnly DateOfBirth):IRequest<Guid>;
}
    