using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.ValueObjects;
using Domain.Enums;
using Application.Services;
using Domain.Exceptions;
using Application.Mapper;

namespace Application.UseCases
{
    public class CreateAdminUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashPasswordHasher _hashPasswordHasher;

        public CreateAdminUserUseCase(IUserRepository userRepository, IHashPasswordHasher hashPasswordHasher)
        {
            _userRepository = userRepository;
            _hashPasswordHasher = hashPasswordHasher;
        }

        public async Task<UserDTO> Execute(CreateAdminUserDTO dto)
        {
            if (await _userRepository.GetByUsername(dto.Username) != null)
                throw new UserInvalidValueException("Username Already exist.");
            if (await _userRepository.GetByEmail(dto.Email) != null)
                throw new UserInvalidValueException("Email Already exist.");
            var hashPassword = _hashPasswordHasher.Hash(dto.Password);

            var user = new User(
                  UsernameValueObject.Create(dto.Username),
                   EmailValueObject.Create(dto.Email),
                   PasswordValueObject.Create(hashPassword),
                   Role.Admin
                );
            await _userRepository.Add(user);
            return UserMapper.ToDto(user);
        }

    }
}
