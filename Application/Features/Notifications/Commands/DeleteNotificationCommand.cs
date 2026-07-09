using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Notifications.Commands
{
    public record DeleteNotificationCommand(Guid NotificationId);
}
