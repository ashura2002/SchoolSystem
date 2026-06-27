using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries
{
    public record GetByIdQuery(Guid UserId);
}
