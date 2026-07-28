using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries
{
    public class GetAllActiveUsersHandler:IRequestHandler<GetAllActiveUserQuery, List<UserDTO>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllActiveUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> Handle(GetAllActiveUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllActiveUsersAsync(request.Page, request.PageSize, cancellationToken);
            return UserMapper.ToResponseList(users);
        }
    }
}
