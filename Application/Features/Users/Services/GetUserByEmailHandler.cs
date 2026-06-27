using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Services
{
    public class GetUserByEmailHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Execute(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(email, cancellationToken) ??
                throw new DomainNotFoundException("User not found");
            return UserMapper.ToDto(user);
        }
    }
}
