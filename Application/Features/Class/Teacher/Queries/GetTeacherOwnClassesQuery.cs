using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Teacher.Queries
{
    public record GetTeacherOwnClassesQuery(int Page, int PageSize);
}
