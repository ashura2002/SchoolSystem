using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Student.Queries
{
    public record GetAllMyClassesQuery(int Page, int PageSize);
}
