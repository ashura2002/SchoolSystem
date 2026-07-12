using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Queries
{
    public class GetLoginUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetLoginUserHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<UserDTO> Handle(GetLoginUserQuery query, CancellationToken cancellationToken)
        {
            _ = query;
            var user = await _userRepository.GetByIdAsync(_currentUserService.UserId, cancellationToken) ??
                       throw new DomainNotFoundException("User not found");
            return UserMapper.ToDto(user);
        }

    }
}
