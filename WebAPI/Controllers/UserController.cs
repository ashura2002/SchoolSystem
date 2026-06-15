using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Requests;


namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CreateAdminUserUseCase _createAdminUserUseCase;

        public UserController(CreateAdminUserUseCase createAdminUserUseCase)
        {
            _createAdminUserUseCase = createAdminUserUseCase;
        }


        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateAdmin(CreateUserRequests requests)
        {
            var newUser = new CreateUserDTO
            {
                Username = requests.Username,
                Email = requests.Email,
                Password = requests.Password,
            };

            var result = await _createAdminUserUseCase.Execute(newUser);
            return Ok(result);
        }

    }
}
