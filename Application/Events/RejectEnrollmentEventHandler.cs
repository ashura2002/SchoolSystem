using Application.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events
{
    public class RejectEnrollmentEventHandler(INotificationRepository notificationRepository,
        ISchoolClassRepository schoolClassRepository, IUnitOfWork unitOfWork)
        : IDomainEventHandler<EnrollmentRejectedDomainEvent>
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly ISchoolClassRepository _schoolClassRepository = schoolClassRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(EnrollmentRejectedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var schoolClass = await _schoolClassRepository.GetClassByIdAsync(domainEvent.ClassId, cancellationToken) ??
                throw new DomainBadRequestException("Class not founc");
            var notification = Notification.Create(domainEvent.StudentId,
                $"You enrollment in {schoolClass.Name.Value} class was rejected by admin.");

            _notificationRepository.Add(notification);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
