using Application.DTOs;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Notifications.Queries
{
    public class GetAllMyNotificationHandler(INotificationRepository notificationRepository,
        ICurrentUserService currentUserService) : IRequestHandler<GetAllMyNotificationQuery, List<NotificationDTO>>
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<List<NotificationDTO>> Handle(GetAllMyNotificationQuery request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetAllMyNotificationsAsync(_currentUserService.UserId, cancellationToken);
            return notification.Select(n => 
            new NotificationDTO(n.Id, 
            n.UserId, 
            n.Content,
            n.IsRead,
            n.CreatedAt, 
            n.UpdatedAt))
            .ToList();
        }
    }
}
