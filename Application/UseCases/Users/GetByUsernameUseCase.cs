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

        public async Task<UserDTO> Execute(string username)
        {
            var user = await _userRepository.GetByUsername(username);
            if (user == null) throw new NotFoundException("User not found");
            return UserMapper.ToDto(user);
        }

    }
}
