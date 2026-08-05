using Application.DTOs;
using Application.Features.Profiles.Commands;
using Application.Features.Profiles.Queries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Constants;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Guid>>> CreateProfile(
            [FromBody] CreateProfileRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateProfileCommand(request.FirstName, request.LastName, request.Address, request.DateOfBirth);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(
                nameof(GetProfileById),
                new { profileId = result },
                new ApiResponse<Guid>
                {
                    Message = "Created successfully",
                    Data = result
                }
                );
        }


        [HttpGet("{profileId}/details")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<UserWithProfileDetailDTO>>> GetProfileById(
            [FromRoute] Guid profileId,
            CancellationToken cancellationToken)
        {
            var query = new GetProfileByIdQuery(profileId);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<UserWithProfileDetailDTO>
            {
                Message = "Profile retrieved successfully",
                Data = result
            };
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateProfile(
            [FromBody] UpdateProfileRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateProfileCommand(
                request.FirstName,
                request.LastName,
                request.Address,
                request.DateOfBirth);

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet("me")]
        public async Task<ActionResult<ApiResponse<UserWithProfileDetailDTO>>> GetMyProfile(
            CancellationToken cancellationToken
            )
        {
            var query = new GetMyProfileQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<UserWithProfileDetailDTO>
            {
                Message = "Profile retrieved successfully",
                Data = result
            };
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ApiResponse<List<UserWithProfileDetailDTO>>>> GetAllProfiles(
           [FromQuery] PaginationRequest request, CancellationToken cancellationToken)
        {
            var query = new GetAllProfilesQuery(request.Page, request.PageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return new ApiResponse<List<UserWithProfileDetailDTO>>
            {
                Message = "Profiles retrieved successfully",
                Data = result
            };
        }

        // Upload profile picture
        [HttpPatch("profile-picture")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateProfilePicture(IFormFile file, CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
                throw new DomainBadRequestException("Please select an image.");

            const long maxFileSize = 5 * 1024 * 1024; // 5 MB

                var allowedTypes = new[]
                {
                    "image/jpeg",
                    "image/png",
                    "image/webp"
                };

            if (file.Length > maxFileSize)
                throw new DomainBadRequestException("Image size cannot exceed 5 MB.");

            if (!allowedTypes.Contains(file.ContentType))
                throw new DomainBadRequestException("Only JPEG, PNG, and WEBP images are allowed.");

            await using var stream = file.OpenReadStream();

            var uploadedImage = await _mediator.Send(
                new UpdateProfilePictureCommand(
                    stream,
                    file.FileName),
                cancellationToken);

            return new ApiResponse<string>
            {
                Message = "Profile picture updated successfully.",
                Data = uploadedImage.Url
            };
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteProfile(CancellationToken cancellationToken)
        {
            var command = new DeleteProfileCommand();
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
