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
        private readonly IUserReadRepository _userReadRepository;

        public GetAllDeactiveUsersHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<List<UserDTO>> Handle(GetAllDeactiveUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userReadRepository.GetAllDeletedUsersAsync(
                request.Page, 
                request.PageSize, 
                cancellationToken);

            return users;
        }
    }
}
