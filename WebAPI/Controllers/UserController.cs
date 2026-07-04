using Application.DTOs;
using Application.Features.Auth.Queries;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WebAPI.Constants;
using WebAPI.DTOs;


namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly GetAllActiveUsersHandler _getAllUsersHandler;
        private readonly GetUserByIdHandler _getUserByIdHandler;
        private readonly GetLoginUserHandler _getLoginUserHandler;
        private readonly UpdateUserHandler _updateUserHandler;
        private readonly DeleteUserHandler _deleteUserHandler;
        private readonly GetAllDeactiveUsersHandler _getAllDeactiveUsersHandler;

        public UserController(GetAllActiveUsersHandler getAllUsersHandler, GetUserByIdHandler getUserByIdHandler,
            GetLoginUserHandler getLoginUserHandler, UpdateUserHandler updateUserHandler, DeleteUserHandler deleteUserHandler,
            GetAllDeactiveUsersHandler getAllDeactiveUsersHandler
            )
        {
            _getAllUsersHandler = getAllUsersHandler;
            _getUserByIdHandler = getUserByIdHandler;
            _getLoginUserHandler = getLoginUserHandler;
            _updateUserHandler = updateUserHandler;
            _deleteUserHandler = deleteUserHandler;
            _getAllDeactiveUsersHandler = getAllDeactiveUsersHandler;
        }

        // admin only
        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetAllActiveUsers(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllActiveUserQuery(request.Page, request.PageSize);
            var result = await _getAllUsersHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<IEnumerable<UserDTO>>
            {
                Message = "Users retrieved successfully",
                Data = result
            });
        }

        [HttpGet("deleted")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetAllUnActiveUsers(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllDeactiveUserQuery(request.Page, request.PageSize);
            var result = await _getAllDeactiveUsersHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<IEnumerable<UserDTO>>
            {
                Message = "Users retrieved successfully",
                Data = result
            });
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Teacher}")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> GetUserById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetByIdQuery(id);
            var result = await _getUserByIdHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<UserDTO>
            {
                Message = "User retrieved successfully",
                Data = result
            });
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserDTO>> GetMe(CancellationToken cancellationToken)
        {
            return Ok(await _getLoginUserHandler.Handle(new GetLoginUserQuery(), cancellationToken));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<UserDTO>>> UpdateUser([FromBody] UpdateUserRequest request, [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new UpdateUserCommand(id, request.Username, request.Password);
            var result = await _updateUserHandler.Handle(command, cancellationToken);
            return Ok(new ApiResponse<UserDTO>
            {
                Message = "Update Successfully",
                Data = result
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteAccount([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand(id);
            await _deleteUserHandler.Handle(command, cancellationToken);
            return NoContent();

        }
    }
}