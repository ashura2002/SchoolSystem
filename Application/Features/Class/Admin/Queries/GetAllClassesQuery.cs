using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Queries
{
    public record GetAllClassesQuery(int Page, int PageSize):IRequest<List<SchoolClassDTO>>;
}
