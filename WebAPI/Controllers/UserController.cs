using Application.DTOs;
using Application.Features.Auth.Queries;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WebAPI.Constants;
using WebAPI.DTOs;


namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // creation of admin
        [HttpPost("admin")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<Guid>>> CreateAdmin([FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
        {
            var command = new CreateAdminCommand(request.Username, request.Email, request.Password);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(
                nameof(GetUserById),
                new { id = result },
                new ApiResponse<Guid>
                {
                    Message = "Created successfully",
                    Data = result
                }
                );
        }

        // creation of teacher
        [HttpPost("teacher")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<Guid>>> CreateTeacher([FromBody] CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateTeacherCommand(request.Username, request.Email, request.Password);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(
                nameof(GetUserById),
                new { id = result },
                new ApiResponse<Guid>
                {
                    Message = "Created successfully",
                    Data = result
                }
               );
        }

        // creation of student
        [HttpPost("student")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<Guid>>> CreateStudent([FromBody] CreateUserRequest request,
         CancellationToken cancellationToken)
        {
            var command = new CreateStudentCommand(request.Username, request.Email, request.Password);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(
                nameof(GetUserById),
                new { id = result },
                new ApiResponse<Guid>
                {
                    Message = "Created successfully",
                    Data = result
                }
              );
        }


        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetAllActiveUsers(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllActiveUserQuery(request.Page, request.PageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<IEnumerable<UserDTO>>
            {
                Message = "Users retrieved successfully",
                Data = result
            };
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("deleted")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetAllUnActiveUsers(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllDeactiveUserQuery(request.Page, request.PageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<IEnumerable<UserDTO>>
            {
                Message = "Users retrieved successfully",
                Data = result
            };
        }

        [EnableRateLimiting(RateLimitPolicies.GetResources)]
        [HttpGet("{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Teacher}")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> GetUserById([FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<UserDTO>
            {
                Message = "User retrieved successfully",
                Data = result
            };
        }

        [HttpGet("me")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> GetMe(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetLoginUserQuery(), cancellationToken);
            return new ApiResponse<UserDTO>
            {
                Message = "User retrieved successfully",
                Data = result
            };
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest request,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new UpdateUserCommand(id, request.Username, request.Password);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteAccount([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand(id);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}