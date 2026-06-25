using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Users
{
    public class GetByUsernameUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetByUsernameUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Execute(string username, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsername(username, cancellationToken) ??
                throw new DomainNotFoundException("User not found");
            return UserMapper.ToDto(user);
        }

    }
}
