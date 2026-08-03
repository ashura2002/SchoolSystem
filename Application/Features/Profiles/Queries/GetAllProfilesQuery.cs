using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Profiles.Queries
{
    public record GetAllProfilesQuery(int Page, int PageSize) : IRequest<List<UserWithProfileDetailDTO>>;
}
