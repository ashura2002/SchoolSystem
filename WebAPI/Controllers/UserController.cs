using Application.DTOs;
using Application.UseCases.Auth;
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
        private readonly GetLoginUserUseCase _getLoginUserUseCase;

        public UserController(GetAllUsersUseCase getAllUsersUseCase, GetUserByIdUseCase getUserByIdUseCase,
            GetLoginUserUseCase getLoginUserUseCase
            )
        {
            _getAllUsersUseCase = getAllUsersUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
            _getLoginUserUseCase = getLoginUserUseCase;
        }

        // admin only
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers([FromRoute] PaginationDTO pagination)
        {
            var users = await _getAllUsersUseCase.Execute(pagination);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult<UserDTO?>> GetUserById([FromRoute] Guid id)
        {
            var user = await _getUserByIdUseCase.Execute(id);
            return Ok(user);
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserDTO>> GetMe()
        {
            return await _getLoginUserUseCase.Execute();
        }

    }


}