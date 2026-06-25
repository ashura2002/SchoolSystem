using Application.DTOs;
using Application.Services;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Auth
{
    public class CreateTeacherUseCase
    {
        private readonly CreateUserService _createUserService;

        public CreateTeacherUseCase(CreateUserService createUserService)
        {
            _createUserService = createUserService;
        }

        public async Task<UserDTO> Execute(CreateUserDTO dto, CancellationToken cancellationToken)
        {
            return await _createUserService.CreateUser(dto, Role.Teacher, cancellationToken);
        }

    }
}
