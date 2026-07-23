using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Notifications.Commands
{
    public class DeleteNotificationHandler(INotificationRepository notification, ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        private readonly INotificationRepository _notificationRepository = notification;
        private readonly ICurrentUserService _currentUserService = currentUser;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(DeleteNotificationCommand command, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.UserId;
            var notification = await _notificationRepository.GetNotificationByIdAsync(command.NotificationId,
                currentUser, cancellationToken) ?? throw new DomainNotFoundException("Notification not found");

            if (notification.UserId != currentUser)
                throw new DomainBadRequestException("You are not authorized to access this notification.");
            _notificationRepository.Remove(notification);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
