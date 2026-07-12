using Application.Features.Users.Commands;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Services
{
    public class UserRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public UserRegistrationService(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateUser(CreateUserCommand dto, Role role, CancellationToken cancellationToken)
        {

            var username = UsernameValueObject.Create(dto.Username);
            var email = EmailValueObject.Create(dto.Email);
            var password = PasswordValueObject.Create(dto.Password);

            if (await _userRepository.GetByUsernameAsync(username.Value, cancellationToken) != null)
                throw new DomainBadRequestException("Username Already Exist");
            if (await _userRepository.GetByEmailAsync(email.Value, cancellationToken) != null)
                throw new DomainBadRequestException("Email Already Exist");
            var hashedPassword = _passwordHasher.Hash(password.Value);

            // create a domain entity
            var user = User.Register(
                username,
                email,
                PasswordValueObject.Create(hashedPassword),
                role
                );

            _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

    }
}
