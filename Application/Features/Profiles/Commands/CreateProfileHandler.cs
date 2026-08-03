using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Profiles.Commands
{
    public class CreateProfileHandler : IRequestHandler<CreateProfileCommand, Guid>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;


        public CreateProfileHandler(
            IProfileRepository profileRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.UserId;
            var user = await _userRepository.GetByIdWithProfileAsync(currentUser, cancellationToken) ??
                throw new DomainNotFoundException("User not found");

            user.CreateProfile(
                FirstNameVO.Create(request.FirstName),
                LastNameVO.Create(request.LastName),
                AddressVO.Create(request.Address),
                request.DateOfBirth);

            _profileRepository.Add(user.Profile!);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return user.Profile!.Id;
        }
    }
}
