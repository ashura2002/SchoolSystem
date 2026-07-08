using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; private set; }
        public string Content { get; private set; }
        public bool IsRead { get; private set; }

        private Notification(Guid userId, string content,
        bool isRead = false)
        {
            UserId = userId;
            Content = content;
            IsRead = isRead;
        }


        public static Notification Create(Guid userId, string content)
        {
            Notification notification = new(userId, content);
            return notification;
        }


        public void MarkAsRead()
        {
            Touch();
        }

        public void MarkAsUnRead()
        {
            Touch();
        }
    }
}
