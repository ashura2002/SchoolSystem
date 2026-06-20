using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Users
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
            var user = await _userRepository.GetById(id) ??
                        throw new NotFoundException("User not found");
            user.UpdateUsername(UsernameValueObject.Create(dto.Username));
            user.UpdatePassword(PasswordValueObject.Create(dto.Password));

            await _userRepository.SaveChangesAsync();
            return UserMapper.ToDto(user);
        }
    }
}
