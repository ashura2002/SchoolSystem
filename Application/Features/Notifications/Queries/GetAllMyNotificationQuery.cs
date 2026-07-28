using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Notifications.Queries
{
    public record GetAllMyNotificationQuery():IRequest<List<NotificationDTO>>;
}
