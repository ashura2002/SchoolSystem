using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Profiles.Queries
{
    public class GetProfileByIdHandler : IRequestHandler<GetProfileByIdQuery, UserWithProfileDetailDTO>
    {
        private readonly IProfileReadRepository _profileReadRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetProfileByIdHandler(
            IProfileReadRepository profileRepository,
            ICurrentUserService currentUserService)
        {
            _profileReadRepository = profileRepository;
            _currentUserService = currentUserService;
        }

        public async Task<UserWithProfileDetailDTO> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.UserId;          
            var profile = await _profileReadRepository.GetProfileByIdAsync(currentUser,
                request.ProfileId,
                cancellationToken) ??
                throw new DomainNotFoundException("Profile not found");

            return profile;
        }
    }
}
