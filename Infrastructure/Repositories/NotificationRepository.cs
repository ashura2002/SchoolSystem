using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class NotificationRepository(AppDbContext context) : INotificationRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Notification>> GetAllMyNotifications(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Notifications
                                 .AsNoTracking()
                                 .Where(n => n.UserId == userId)
                                 .ToListAsync(cancellationToken);

        }

        public async Task<Notification?> GetNotificationById(Guid notificationId, Guid userId,
            CancellationToken cancellationToken)
        {
            return await _context.Notifications
                                 .FirstOrDefaultAsync(n => n.Id == notificationId &&
                                 n.UserId == userId, cancellationToken);
        }

        void INotificationRepository.Add(Notification notification)
        {
            _context.Notifications.Add(notification);
        }

        void INotificationRepository.Remove(Notification notification)
        {
            _context.Notifications.Remove(notification);
        }
    }
}
