using Application.Features.Users.Commands;


namespace WebAPI.DTOs
{
    public static class UserRequestMapper
    {
        public static CreateUserCommand ToDTO(CreateUserRequests request)
        {
            return new CreateUserCommand(request.Username, request.Email, request.Password);
        }
    }
}
