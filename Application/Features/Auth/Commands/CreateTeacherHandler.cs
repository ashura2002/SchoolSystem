using Application.DTOs;
using Application.Features.Auth.Services;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands
{
    public class CreateTeacherHandler
    {
        private readonly CreateUserService _createUserService;

        public CreateTeacherHandler(CreateUserService createUserService)
        {
            _createUserService = createUserService;
        }

        public async Task<UserDTO> Execute(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await _createUserService.CreateUser(command, Role.Teacher, cancellationToken);
        }

    }
}
