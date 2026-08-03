using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Profiles.Commands
{
    public class UpdateProfilePictureHandler : IRequestHandler<UpdateProfilePictureCommand, UploadedImage>
    {
        private readonly IImageStorage _imageStorage;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateProfilePictureHandler> _logger;

        public UpdateProfilePictureHandler(
            IImageStorage iImageStorage,
            IUserRepository userRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            ILogger<UpdateProfilePictureHandler> logger)
        {
            _imageStorage = iImageStorage;
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UploadedImage> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;
            var user = await _userRepository.GetByIdWithProfileAsync(currentUserId, cancellationToken);
            if(user == null)
                throw new DomainNotFoundException("User not found");

            // for existed profile picture public id
            string? oldProfilePicturePublicId = user.Profile?.ProfilePicturePublicId;

            UploadedImage newProfilePicture = await _imageStorage.UploadPictureAsync(
                request.Stream,
                request.FileName,
                cancellationToken);


            try
            {
                user.UpdateProfilePicture(newProfilePicture.Url, newProfilePicture.PublicId);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await RollbackUploadedImageAsync(newProfilePicture.PublicId, ex, cancellationToken);
                throw;
            }


            await DeleteOldProfilePictureAsync(oldProfilePicturePublicId, cancellationToken);

            return newProfilePicture;
        }

        // private helpers
        private async Task RollbackUploadedImageAsync(string publicId, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Failed to update profile picture for user {UserId}", 
                _currentUserService.UserId);

            try
            {
                await _imageStorage.DeleteAsync(publicId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to rollback uploaded image {imageurl}", publicId);
            }
        }


        private async Task DeleteOldProfilePictureAsync(string? oldProfilePicturePublicId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(oldProfilePicturePublicId)) return;

            try
            {
                await _imageStorage.DeleteAsync(oldProfilePicturePublicId, cancellationToken);
            }catch(Exception ex)
            {
                _logger.LogWarning(ex, "Failed to delete old image {imageUrl}", oldProfilePicturePublicId);
            }
        }
    }
}
