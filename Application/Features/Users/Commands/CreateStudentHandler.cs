using Application.DTOs;
using Application.Features.Auth.Services;
using Application.Interfaces;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands
{
    public class CreateStudentHandler:IRequestHandler<CreateStudentCommand, Guid>
    {
        private readonly UserRegistrationService _createUserService;

        public CreateStudentHandler(UserRegistrationService createStudentService)
        {
            _createUserService = createStudentService;
        }

        public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            return await _createUserService.CreateUser(
                request.Username,
                request.Email,
                request.Password,
                Role.Student, 
                cancellationToken);
        }
    }
}
