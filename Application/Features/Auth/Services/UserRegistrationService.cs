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
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public UserRegistrationService(
            IUserReadRepository userReadRepository,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher, 
            IUnitOfWork unitOfWork)
        {
            _userReadRepository = userReadRepository;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateUser(
            string username,
            string email,
            string password,
            Role role, 
            CancellationToken cancellationToken)
        {

            var usernameVo = UsernameVO.Create(username);
            var emailVo = EmailVO.Create(email);
            var passwordVo = PasswordVO.Create(password);

            if (await _userReadRepository.IsUsernameExistsAsync(usernameVo.Value, cancellationToken))
                throw new DomainBadRequestException("Username already exists.");

            if (await _userReadRepository.IsEmailExistsAsync(emailVo.Value, cancellationToken))
                throw new DomainBadRequestException("Email already exist");

            var hashedPassword = _passwordHasher.Hash(passwordVo.Value);

            // create a domain entity
            var user = User.Register(
                usernameVo,
                emailVo,
                PasswordVO.Create(hashedPassword),
                role
                );

            _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

    }
}
