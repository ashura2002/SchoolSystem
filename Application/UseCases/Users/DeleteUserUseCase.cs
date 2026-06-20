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

        public DeleteUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(Guid id)
        {
            var user = await _userRepository.GetById(id)
                ?? throw new NotFoundException("User not found");
            await _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }

    }
}
