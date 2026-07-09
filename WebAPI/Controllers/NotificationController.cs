using Application.DTOs;
using Application.Features.Notifications.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly GetAllMyNotificationHandler _getAllMyNotificationHandler;

        public NotificationController(GetAllMyNotificationHandler getAllMyNotificationHandler)
        {
            _getAllMyNotificationHandler = getAllMyNotificationHandler;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<NotificationDTO>>>> GetAllMyNotifications(
            CancellationToken cancellationToken)
        {
            GetAllMyNotificationQuery query = new();
            var notifications = await _getAllMyNotificationHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<IEnumerable<NotificationDTO>>
            {
                Message = "Notification retrieved successfully",
                Data = notifications
            });
        }
    }
}
