using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Notifications.Commands
{
    public class MarkAsUnreadNotificationHandler(INotificationRepository notificationRepository,
        ICurrentUserService currentUserService, IUnitOfWork unitOfWork):IRequestHandler<MarkAsUnreadNotificationCommand>
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(MarkAsUnreadNotificationCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.UserId;
            var notification = await _notificationRepository.GetNotificationByIdAsync(request.NotificationId, currentUser, cancellationToken)
                 ?? throw new DomainNotFoundException("Notification not found");
            if (notification.UserId != currentUser)
                throw new DomainBadRequestException("You are not authorized to access this notification.");

            notification.MarkAsUnRead();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
