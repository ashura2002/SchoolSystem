using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface INotificationRepository
    {
        void Add(Notification notification);
        void Remove(Notification notification);
        Task<List<Notification>> GetAllMyNotificationsAsync(Guid userId, CancellationToken cancellationToken);
        Task<Notification?> GetNotificationByIdAsync(Guid notificationId, Guid userId, CancellationToken cancellationToken);
    }
}
