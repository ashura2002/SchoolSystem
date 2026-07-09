using Application.Interfaces;
using Domain.Entities;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events
{
    public class CreateNotificationEventHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
        : IDomainEventHandler<EnrollmentRequestedDomainEvent>
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task Handle(EnrollmentRequestedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            Console.WriteLine("Event executing... CreateNotificationEvent Handler");
            var notification = Notification.Create(domainEvent.StudentId, "You successfully submitted the enrollment.");
            _notificationRepository.Add(notification);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
