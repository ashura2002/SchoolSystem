using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Users
{
    public class GetAllDeactiveUsers
    {
        private readonly IUserRepository _userRepository;

        public GetAllDeactiveUsers(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> Execute(PaginationDTO pagination,CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllDeletedUsers(pagination,cancellationToken);
            return UserMapper.ToResponseList(users);
        }

    }
}
