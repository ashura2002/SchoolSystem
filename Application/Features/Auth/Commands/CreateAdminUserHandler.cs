using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;
using Application.Features.Auth.Services;


namespace Application.Features.Auth.Commands
{
    public class CreateAdminUserHandler
    {
        private readonly CreateUserService _createUserService;

        public CreateAdminUserHandler(CreateUserService createUserService)
        {
            _createUserService = createUserService;
        }

        public async Task<UserDTO> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await _createUserService.CreateUser(command, Role.Admin, cancellationToken);
        }

    }
}
