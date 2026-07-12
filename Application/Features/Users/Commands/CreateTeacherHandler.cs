using Application.DTOs;
using Application.Features.Auth.Services;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands
{
    public class CreateTeacherHandler
    {
        private readonly UserRegistrationService _createUserService;

        public CreateTeacherHandler(UserRegistrationService createUserService)
        {
            _createUserService = createUserService;
        }

        public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await _createUserService.CreateUser(command, Role.Teacher, cancellationToken);
        }

    }
}
