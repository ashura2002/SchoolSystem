using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Notifications.Commands
{
    public class MarkAsUnreadNotificationHandler(INotificationRepository notificationRepository,
        ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<NotificationDTO> Handle(MarkAsUnreadNotificationCommand command, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.UserId;
            var notification = await _notificationRepository.GetNotificationById(command.NotificationId, currentUser, cancellationToken)
                 ?? throw new DomainNotFoundException("Notification not found");
            if (notification.UserId != currentUser)
                throw new DomainBadRequestException("You are not authorized to access this notification.");

            notification.MarkAsUnRead();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new NotificationDTO(notification.Id, notification.UserId, notification.Content, notification.IsRead,
                notification.CreatedAt, notification.UpdatedAt);
        }
    }
}
