using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CreateUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public  async Task<UserDTO> CreateUser(CreateUserDTO dto, Role role)
        {
            if (await _userRepository.GetByUsername(dto.Username) != null)
                throw new UserInvalidValueException("Username Already Exist");
            if (await _userRepository.GetByEmail(dto.Email) != null)
                throw new UserInvalidValueException("Email Already Exist");
            var hashedPassword = _passwordHasher.Hash(dto.Password);
            // create a domain entity
            var user = new User(
                UsernameValueObject.Create(dto.Username),
                EmailValueObject.Create(dto.Email),
                PasswordValueObject.Create(hashedPassword),
                role
                );
            await _userRepository.Add(user);
            Console.WriteLine("CREATED USER " + user);
            return UserMapper.ToDto(user);
        }

    }
}
