using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class CreateTeacherUseCase
    {
        private readonly CreateUserService _createUserService;

        public CreateTeacherUseCase(CreateUserService createUserService)
        {
            _createUserService = createUserService;
        }

        public async Task<UserDTO> Execute(CreateUserDTO dto)
        {
            return await _createUserService.CreateUser(dto, Role.Teacher);
        }

    }
}
