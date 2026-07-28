using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Notifications.Commands
{
    public class MarkAsReadNotificationHandler(INotificationRepository notificationRepository, ICurrentUserService currentUser,
        IUnitOfWork unitOfWork):IRequestHandler<MarkAsReadNotificationCommand>
    {
        private readonly INotificationRepository _repo = notificationRepository;
        private readonly ICurrentUserService _currentUser = currentUser;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(MarkAsReadNotificationCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUser.UserId;
            var notification = await _repo.GetNotificationByIdAsync(request.NotificationId, currentUser, cancellationToken) ??
                throw new DomainNotFoundException("Notification not found");
            if (notification.UserId != currentUser)
                throw new DomainBadRequestException("You are not authorized to access this notification.");

            notification.MarkAsRead();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
