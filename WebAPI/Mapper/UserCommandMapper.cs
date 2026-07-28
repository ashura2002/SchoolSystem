using Application.Features.Users.Commands;


namespace WebAPI.DTOs
{
    public static class UserCommandMapper
    {
        public static CreateUserCommand ToCommand(CreateUserRequest request)
        {
            return new CreateUserCommand(request.Username, 
                request.Email, 
                request.Password);
        }
    }
}
