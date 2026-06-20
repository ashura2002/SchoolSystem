using Application.DTOs;
using Application.UseCases.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly GetAllUsersUseCase _getAllUsersUseCase;
        private readonly GetUserByIdUseCase _getUserByIdUseCase;

        public UserController(GetAllUsersUseCase getAllUsersUseCase, GetUserByIdUseCase getUserByIdUseCase)
        {
            _getAllUsersUseCase = getAllUsersUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
        }

        // admin only
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var users = await _getAllUsersUseCase.Execute();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<ActionResult<UserDTO?>> GetUserById([FromRoute] Guid id)
        {
            var user = await _getUserByIdUseCase.Execute(id);
            return Ok(user);
        }

    }


}