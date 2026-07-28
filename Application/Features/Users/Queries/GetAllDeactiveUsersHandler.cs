using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries
{
    public class GetAllDeactiveUsersHandler : IRequestHandler<GetAllDeactiveUserQuery, List<UserDTO>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllDeactiveUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> Handle(GetAllDeactiveUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllDeletedUsersAsync(request.Page, request.PageSize, cancellationToken);
            return UserMapper.ToResponseList(users);
        }
    }
}
