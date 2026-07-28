using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries
{
    public class GetUserByIdHandler:IRequestHandler<GetByIdQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken) ??
                throw new DomainNotFoundException("User not found");
            return UserMapper.ToDto(user);
        }
    }
}
