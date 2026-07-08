using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class NotificationRepository(AppDbContext context) : INotificationRepository
    {
        private readonly AppDbContext _context = context;

        public Task<List<Notification>> GetAllMyNotification()
        {
            throw new NotImplementedException();
        }

        void INotificationRepository.Add(Notification notification)
        {
            _context.Add(notification);
        }

        void INotificationRepository.Remove(Notification notification)
        {
            _context.Remove(notification);
        }
    }
}
