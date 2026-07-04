using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries
{
    public class GetAllActiveUsersHandler
    {
        private readonly IUserRepository _userRepository;

        public GetAllActiveUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> Handle(GetAllActiveUserQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllActiveUsers(query.Page, query.PageSize, cancellationToken);
            return UserMapper.ToResponseList(users);
        }
    }
}
