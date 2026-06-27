using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands
{
    public class DeleteUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteUserHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(command.UserId, cancellationToken)
                ?? throw new DomainNotFoundException("User not found");
            if (user.Id == _currentUserService.UserId)
                throw new DomainBadRequestException("You cannot delete your account");
            user.DeactivateAccount();
            await _userRepository.SaveChangesAsync(cancellationToken);
        }

    }
}
