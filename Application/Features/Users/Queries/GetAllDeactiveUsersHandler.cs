using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries
{
    public class GetAllDeactiveUsersHandler
    {
        private readonly IUserRepository _userRepository;

        public GetAllDeactiveUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> Handle(GetAllDeactiveUserQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllDeletedUsersAsync(query.Page, query.PageSize, cancellationToken);
            return UserMapper.ToResponseList(users);
        }

    }
}
