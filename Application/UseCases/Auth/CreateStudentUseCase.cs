using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Auth
{
    public class CreateStudentUseCase
    {
        private readonly CreateUserService _createUserService;

        public CreateStudentUseCase(CreateUserService createStudentService)
        {
            _createUserService = createStudentService;
        }

        public async Task<UserDTO> Execute(CreateUserDTO dto)
        {
            return await _createUserService.CreateUser(dto, Role.Student);
        }

    }
}
