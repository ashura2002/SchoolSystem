using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class UpdateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Execute(Guid id, UpdateUserDTO dto)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) throw new UserNotFoundException("User not found");
            user.UpdateUsername(UsernameValueObject.Create(dto.Username));
            user.UpdatePassword(PasswordValueObject.Create(dto.Password));
            await _userRepository.Update(user);
            return UserMapper.ToDto(user);
        }
    }
}
