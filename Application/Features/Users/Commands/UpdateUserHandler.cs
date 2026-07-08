using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands
{
    public class UpdateUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDTO> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetById(command.UserId, cancellationToken) ??
                        throw new DomainNotFoundException("User not found");
            var hashedPassword = _passwordHasher.Hash(command.Password);
            user.UpdateUsername(UsernameValueObject.Create(command.Username));
            user.UpdatePassword(PasswordValueObject.Create(hashedPassword));

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return UserMapper.ToDto(user);
        }
    }
}
