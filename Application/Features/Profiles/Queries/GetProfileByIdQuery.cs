using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Profiles.Queries
{
    public record GetProfileByIdQuery(Guid ProfileId):IRequest<UserWithProfileDetailDTO>;
}
