using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Notifications.Queries
{
    public class GetAllMyNotificationHandler(INotificationRepository notificationRepository,
        ICurrentUserService currentUserService)
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<List<NotificationDTO>> Handle(GetAllMyNotificationQuery _, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;
            var notifications = await _notificationRepository.GetAllMyNotifications(currentUserId, cancellationToken);
            return notifications.Select(n =>
            new NotificationDTO(n.Id, n.UserId, n.Content, n.IsRead, n.CreatedAt, n.UpdatedAt)).ToList();
        }
    }
}
