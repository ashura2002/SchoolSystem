using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
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
            var user = await _userRepository.GetById(id);
            if (user == null) throw new UserNotFoundException("User not found");
            await _userRepository.Delete(user);
        }

    }
}
