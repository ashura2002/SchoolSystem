using Application.DTOs;
using Application.Features.Notifications.Commands;
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
        private readonly GetNotificationByIdHandler _getNotificationByIdHandler;
        private readonly MarkAsReadNotificationHandler _markAsReadNotificationHandler;
        private readonly MarkAsUnreadNotificationHandler _markAsUnreadNotificationHandler;
        private readonly DeleteNotificationHandler _deleteNotificationHandler;

        public NotificationController(GetAllMyNotificationHandler getAllMyNotificationHandler,
            GetNotificationByIdHandler getNotificationByIdHandler, MarkAsReadNotificationHandler markAsReadNotificationHandler,
            MarkAsUnreadNotificationHandler markAsUnreadNotificationHandler, DeleteNotificationHandler deleteNotificationHandler)
        {
            _getAllMyNotificationHandler = getAllMyNotificationHandler;
            _getNotificationByIdHandler = getNotificationByIdHandler;
            _markAsReadNotificationHandler = markAsReadNotificationHandler;
            _markAsUnreadNotificationHandler = markAsUnreadNotificationHandler;
            _deleteNotificationHandler = deleteNotificationHandler;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<NotificationDTO>>> GetNotificationById(
        [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            GetNotificationByIdQuery query = new(id);
            var result = await _getNotificationByIdHandler.Handle(query, cancellationToken);
            return Ok(new ApiResponse<NotificationDTO>
            {
                Message = "Retrieved sucessfully",
                Data = result
            });
        }

        [HttpPut("{id}/read")]
        public async Task<ActionResult<ApiResponse<NotificationDTO>>> MarkAsRead([FromRoute] Guid id,
          CancellationToken cancellationToken)
        {
            MarkAsReadNotificationCommand command = new(id);
            var result = await _markAsReadNotificationHandler.Handle(command, cancellationToken);
            return Ok(new ApiResponse<NotificationDTO>
            {
                Message = "Notification marked as read successfully.",
                Data = result
            });
        }

        [HttpPut("{id}/unread")]
        public async Task<ActionResult<ApiResponse<NotificationDTO>>> MarkAsUnread([FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            MarkAsUnreadNotificationCommand command = new(id);
            var result = await _markAsUnreadNotificationHandler.Handle(command, cancellationToken);
            return Ok(new ApiResponse<NotificationDTO>
            {
                Message = "Notification marked as unread successfully.",
                Data = result
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
        {
            DeleteNotificationCommand command = new(id);

            await _deleteNotificationHandler.Handle(command, cancellationToken);

            return NoContent();
        }
    }
}
