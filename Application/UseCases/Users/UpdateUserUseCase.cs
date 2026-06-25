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
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDTO> Execute(Guid id, UpdateUserDTO dto, CancellationToken cancellationToken)
        {

            var hashPassword = _passwordHasher.Hash(dto.Password);
            var user = await _userRepository.GetById(id, cancellationToken) ??
                        throw new DomainNotFoundException("User not found");
            user.UpdateUsername(UsernameValueObject.Create(dto.Username));
            user.UpdatePassword(PasswordValueObject.Create(hashPassword));

            await _userRepository.SaveChangesAsync(cancellationToken);
            return UserMapper.ToDto(user);
        }
    }
}
