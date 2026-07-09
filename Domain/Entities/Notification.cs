using Domain.Enums;
using Domain.Exceptions;
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
            if (IsRead)
                throw new DomainBadRequestException("This notification is already read");

            IsRead = true;
            Touch();
        }

        public void MarkAsUnRead()
        {
            if (!IsRead)
                throw new DomainBadRequestException("This notification is already unread.");
            IsRead = false;
            Touch();
        }
    }
}
