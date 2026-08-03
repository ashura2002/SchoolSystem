using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Profiles.Queries
{
    public class GetMyProfileHandler : IRequestHandler<GetMyProfileQuery, UserWithProfileDetailDTO>
    {
        private readonly IProfileReadRepository _profileReadRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetMyProfileHandler(
            IProfileReadRepository profileReadRepository,
            ICurrentUserService currentUserService
            )
        {
            _profileReadRepository = profileReadRepository;
            _currentUserService = currentUserService;
        }

        public async Task<UserWithProfileDetailDTO> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.UserId;
            var profile = await _profileReadRepository.GetMyProfileAsync(currentUser, cancellationToken) ??
                throw new DomainNotFoundException("Profile not found");

            return profile;
        }
    }
}
