using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Users
{
    public class GetAllActiveUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetAllActiveUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> Execute(PaginationDTO pagination)
        {
            var users = await _userRepository.GetAllActiveUsers(pagination);
            return UserMapper.ToResponseList(users);
        }
    }
}
