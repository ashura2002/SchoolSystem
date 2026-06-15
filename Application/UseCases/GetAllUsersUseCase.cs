using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class GetAllUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> Execute()
        {
            var users = await _userRepository.GetAllUsers();
            return users.Select(user => UserMapper.ToDto(user)).ToList();
        }

    }
}
