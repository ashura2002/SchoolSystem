using Application.DTOs;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Profiles.Queries
{
    public class GetAllProfilesHandler : IRequestHandler<GetAllProfilesQuery, List<UserWithProfileDetailDTO>>
    {
        private readonly IProfileReadRepository _profileReadRepository;

        public GetAllProfilesHandler(
            IProfileReadRepository profileReadRepository)
        {
            _profileReadRepository = profileReadRepository;
        }


        public async Task<List<UserWithProfileDetailDTO>> Handle(GetAllProfilesQuery request, CancellationToken cancellationToken)
        {
            var profiles = await _profileReadRepository.GetAllProfiles(
                request.Page, 
                request.PageSize, 
                cancellationToken);

            return profiles;
        }
    }
}
