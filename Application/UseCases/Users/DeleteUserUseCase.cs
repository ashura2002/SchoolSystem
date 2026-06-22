using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Users
{
    public class DeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteUserUseCase(IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task Execute(Guid id)
        {
            var user = await _userRepository.GetById(id)
                ?? throw new NotFoundException("User not found");
            if (user.Id == _currentUserService.UserId)
                throw new BadRequestException("You cannot delete your account");
            user.DeactivateAccount();
            await _userRepository.SaveChangesAsync();
        }

    }
}
