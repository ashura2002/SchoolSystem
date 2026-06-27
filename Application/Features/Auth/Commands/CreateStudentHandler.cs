using Application.DTOs;
using Application.Features.Auth.Services;
using Application.Interfaces;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands
{
    public class CreateStudentHandler
    {
        private readonly CreateUserService _createUserService;

        public CreateStudentHandler(CreateUserService createStudentService)
        {
            _createUserService = createStudentService;
        }

        public async Task<UserDTO> Execute(CreateUserCommand command,CancellationToken cancellationToken)
        {
            return await _createUserService.CreateUser(command, Role.Student,cancellationToken);
        }

    }
}
