using Application.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Profiles.Commands
{
    public class DeleteProfileHandler : IRequestHandler<DeleteProfileCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IImageStorage _imageStorage;
        private readonly ILogger<DeleteProfileHandler> _logger;

        public DeleteProfileHandler(
            IUserRepository userRepository,
            IProfileRepository profileRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            IImageStorage imageStorage,
            ILogger<DeleteProfileHandler> logger)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _imageStorage = imageStorage;
            _logger = logger;
        }

        public async Task Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;
            var user = await _userRepository.GetByIdWithProfileAsync(currentUserId, cancellationToken)??
                throw new DomainNotFoundException("User not found");

            var profile = user.DeleteProfile();
            _profileRepository.Remove(profile);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Delete the image after SaveChanges().
            // If Cloudinary deletion fails, the profile is still deleted,
            // and the orphan image can be cleaned up later.
            if (!string.IsNullOrWhiteSpace(profile.ProfilePicturePublicId))
            {
                try
                {
                    await _imageStorage.DeleteAsync(profile.ProfilePicturePublicId, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, 
                        "Failed to delete profile picture in Cloudinary {PublicId}",
                        profile.ProfilePicturePublicId);
                }
            }
        }
    }
}
