using Application.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events
{
    public class StudentNotificationEventHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork,
         ISchoolClassRepository schoolClassRepository)
        : IDomainEventHandler<EnrollmentRequestedDomainEvent>
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ISchoolClassRepository _schoolClassRepository = schoolClassRepository;

        public async Task Handle(EnrollmentRequestedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var schoolClass = await _schoolClassRepository.GetClassByIdAsync(domainEvent.ClassId, cancellationToken) ??
               throw new DomainNotFoundException("Class not found");

            var notification = Notification.Create(domainEvent.StudentId,
                $"You successfully submitted the enrollment in {schoolClass.Name.Value} class.");

            _notificationRepository.Add(notification);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
