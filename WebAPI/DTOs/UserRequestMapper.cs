using Application.DTOs;
using WebAPI.Requests;


namespace WebAPI.DTOs
{
    public static class UserRequestMapper
    {
        public static CreateUserDTO ToDTO(CreateUserRequests request)
        {
            return new CreateUserDTO
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
            };
        }
    }
}
