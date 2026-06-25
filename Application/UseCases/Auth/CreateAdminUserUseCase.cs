using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.ValueObjects;
using Application.Services;
using Domain.Enums;


namespace Application.UseCases.Auth
{
    public class CreateAdminUserUseCase
    {
        private readonly CreateUserService _createUserService;

        public CreateAdminUserUseCase(CreateUserService createUserService)
        {
            _createUserService = createUserService;
        }

        public async Task<UserDTO> Execute(CreateUserDTO dto, CancellationToken cancellationToken)
        {
            return await _createUserService.CreateUser(dto, Role.Admin, cancellationToken);
        }

    }
}
