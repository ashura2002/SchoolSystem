using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;
using Application.Features.Auth.Services;

namespace Application.Features.Users.Commands
{
    public class CreateAdminUserHandler
    {
        private readonly UserRegistrationService _createUserService;

        public CreateAdminUserHandler(UserRegistrationService createUserService)
        {
            _createUserService = createUserService;
        }

        public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await _createUserService.CreateUser(command, Role.Admin, cancellationToken);
        }

    }
}
