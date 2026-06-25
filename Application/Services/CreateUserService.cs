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

        public async Task<UserDTO> CreateUser(CreateUserDTO dto, Role role, CancellationToken cancellationToken)
        {

            var username = UsernameValueObject.Create(dto.Username);
            var email = EmailValueObject.Create(dto.Email);
            var password = PasswordValueObject.Create(dto.Password);

            if (await _userRepository.GetByUsername(username.Value, cancellationToken) != null)
                throw new DomainBadRequestException("Username Already Exist");
            if (await _userRepository.GetByEmail(email.Value, cancellationToken) != null)
                throw new DomainBadRequestException("Email Already Exist");
            var hashedPassword = _passwordHasher.Hash(password.Value);

            // create a domain entity
            var user = new User(
                username,
                email,
                PasswordValueObject.Create(hashedPassword),
                role
                );

            await _userRepository.Add(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return UserMapper.ToDto(user);
        }

    }
}
