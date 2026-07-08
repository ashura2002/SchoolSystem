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
        Task<List<Notification>> GetAllMyNotification();
    }
}
