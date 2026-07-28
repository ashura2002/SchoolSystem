using Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WebAPI.Constants;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(
          IMediator mediator
          )
        {
            _mediator = mediator;
        }


        [EnableRateLimiting(RateLimitPolicies.Login)]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommand(request.Username, request.Password);
            var result = await _mediator.Send(command,cancellationToken);
            return new ApiResponse<string>
            {
                Message = "Login successfully",
                Data = result
            };
        }

    }
}
