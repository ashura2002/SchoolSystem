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
        private readonly IUserReadRepository _userReadRepository;

        public GetUserByEmailHandler(IUserReadRepository userRepository)
        {
            _userReadRepository = userRepository;
        }

        public async Task<UserDTO> Execute(string email, CancellationToken cancellationToken)
        {
            var user = await _userReadRepository.GetByEmailAsync(email, cancellationToken) ??
                throw new DomainNotFoundException("User not found");
            return user;
        }
    }
}
