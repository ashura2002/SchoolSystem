using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Notifications.Queries
{
    public class GetNotificationByIdHandler(INotificationRepository notificationRepository, ICurrentUserService currentUserService)
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<NotificationDTO> Handle(GetNotificationByIdQuery query, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.UserId;
            var notification = await _notificationRepository.GetNotificationByIdAsync(query.NotificationId, currentUser,
                cancellationToken) ??
                throw new DomainNotFoundException("Notification not found");

            return new NotificationDTO(notification.Id, notification.UserId, notification.Content,
                notification.IsRead, notification.CreatedAt, notification.UpdatedAt);
        }
    }
}
