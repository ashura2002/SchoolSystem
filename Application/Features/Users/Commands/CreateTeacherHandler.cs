using Application.DTOs;
using Application.Features.Auth.Services;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands
{
    public class CreateTeacherHandler:IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly UserRegistrationService _createUserService;

        public CreateTeacherHandler(UserRegistrationService createUserService)
        {
            _createUserService = createUserService;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _createUserService.CreateUser(request, Role.Teacher, cancellationToken);
        }

    }
}
