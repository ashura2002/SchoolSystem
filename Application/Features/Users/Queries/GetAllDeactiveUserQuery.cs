using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries
{
    public record GetAllDeactiveUserQuery(int Page, int PageSize);
}
