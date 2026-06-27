using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries
{
    public class GetAllActiveUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetAllActiveUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> Execute(GetAllActiveUserQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllActiveUsers(query.Page, query.PageSize, cancellationToken);
            return UserMapper.ToResponseList(users);
        }
    }
}
