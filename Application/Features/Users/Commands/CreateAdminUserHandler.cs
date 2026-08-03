using Domain.Enums;
using Application.Features.Auth.Services;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class CreateAdminUserHandler:IRequestHandler<CreateAdminCommand, Guid>
    {
        private readonly UserRegistrationService _createUserService;

        public CreateAdminUserHandler(UserRegistrationService createUserService)
        {
            _createUserService = createUserService;
        }

        public async Task<Guid> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            return await _createUserService.CreateUser(
                request.Username,
                request.Email,
                request.Password,
                Role.Admin,
                cancellationToken
                );
        }
    }
}
