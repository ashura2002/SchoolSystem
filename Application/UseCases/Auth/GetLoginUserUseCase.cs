using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Auth
{
    public class GetLoginUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetLoginUserUseCase(IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<UserDTO> Execute(CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUserService.UserId, cancellationToken) ?? 
                       throw new DomainNotFoundException("User not found");
            return UserMapper.ToDto(user);
        }

    }
}
