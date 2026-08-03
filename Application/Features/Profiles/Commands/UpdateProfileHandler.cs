using Application.Interfaces;
using Domain.Exceptions;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Profiles.Commands
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProfileHandler(
            IUserRepository userRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }


        public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var firstNameVo = FirstNameVO.Create(request.FirstName);
            var lastNameVo = LastNameVO.Create(request.LastName);
            var addressVo = AddressVO.Create(request.Address);

            var currentUser = _currentUserService.UserId;
            var user = await _userRepository.GetByIdWithProfileAsync(currentUser, cancellationToken) ??
                throw new DomainNotFoundException("User not found.");

            user.UpdateProfile(
                firstNameVo,
                lastNameVo,
                addressVo,
                request.DateOfBirth
                );

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
