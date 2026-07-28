using Domain.Enums;
using Application.Features.Auth.Services;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class CreateAdminUserHandler:IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly UserRegistrationService _createUserService;

        public CreateAdminUserHandler(UserRegistrationService createUserService)
        {
            _createUserService = createUserService;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _createUserService.CreateUser(request, Role.Admin, cancellationToken);
        }

    }
}
