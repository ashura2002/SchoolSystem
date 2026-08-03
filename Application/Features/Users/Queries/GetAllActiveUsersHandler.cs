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
        private readonly IUserReadRepository _userReadRepository;

        public GetAllActiveUsersHandler(IUserReadRepository userRepository)
        {
            _userReadRepository = userRepository;
        }

        public async Task<List<UserDTO>> Handle(GetAllActiveUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userReadRepository.GetAllActiveUsersAsync(
                request.Page, 
                request.PageSize, 
                cancellationToken);

            return users;
        }
    }
}
