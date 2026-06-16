using Application.DTOs;
using WebAPI.Requests;


namespace WebAPI.DTOs
{
    public static class UserRequestMapper
    {
        public static CreateUserDTO ToDTO(CreateUserRequests request)
        {
            return new CreateUserDTO(request.Username, request.Email, request.Password);
        }
    }
}
