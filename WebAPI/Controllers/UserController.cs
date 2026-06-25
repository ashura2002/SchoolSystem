using Application.DTOs;
using Application.UseCases.Auth;
using Application.UseCases.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;


namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly GetAllActiveUsersUseCase _getAllUsersUseCase;
        private readonly GetUserByIdUseCase _getUserByIdUseCase;
        private readonly GetLoginUserUseCase _getLoginUserUseCase;
        private readonly UpdateUserUseCase _updateUserUseCase;
        private readonly DeleteUserUseCase _deleteUserUseCase;
        private readonly GetAllDeactiveUsers _getAllDeactiveUsers;

        public UserController(GetAllActiveUsersUseCase getAllUsersUseCase, GetUserByIdUseCase getUserByIdUseCase,
            GetLoginUserUseCase getLoginUserUseCase, UpdateUserUseCase updateUserUseCase, DeleteUserUseCase deleteUserUseCase,
            GetAllDeactiveUsers getAllDeactiveUsers
            )
        {
            _getAllUsersUseCase = getAllUsersUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
            _getLoginUserUseCase = getLoginUserUseCase;
            _updateUserUseCase = updateUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
            _getAllDeactiveUsers = getAllDeactiveUsers;
        }

        // admin only
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDTO>>> GetAllActiveUsers([FromQuery] PaginationDTO pagination,
            CancellationToken cancellationToken)
        {
            var users = await _getAllUsersUseCase.Execute(pagination, cancellationToken);
            return Ok(users);
        }

        [HttpGet("deleted")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUnActiveUsers([FromQuery] PaginationDTO pagination,
            CancellationToken cancellationToken)
        {
            var users = await _getAllDeactiveUsers.Execute(pagination, cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult<UserDTO?>> GetUserById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var user = await _getUserByIdUseCase.Execute(id, cancellationToken);
            return Ok(user);
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserDTO>> GetMe(CancellationToken cancellationToken)
        {
            return Ok(await _getLoginUserUseCase.Execute(cancellationToken));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> UpdateUser([FromBody] UpdateUserRequest request, [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var user = new UpdateUserDTO(request.Username, request.Password);
            var result = await _updateUserUseCase.Execute(id, user, cancellationToken);
            return Ok(new ApiResponse<UserDTO>
            {
                Message = "Update Successfully",
                Data = result
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAccount([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _deleteUserUseCase.Execute(id, cancellationToken);
            return NoContent();

        }
    }
}