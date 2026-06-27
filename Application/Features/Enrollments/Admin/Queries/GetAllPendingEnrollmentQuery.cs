using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Admin.Queries
{
    public record GetAllPendingEnrollmentQuery(int Page, int PageSize);
}
