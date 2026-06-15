using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class GetUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Execute(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) throw new UserNotFoundException("User Not Found");
            return UserMapper.ToDto(user);
        }

    }
}
