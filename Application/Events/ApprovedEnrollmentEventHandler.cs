using Application.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events
{
    public class ApprovedEnrollmentEventHandler(INotificationRepository notificationRepository,
        ISchoolClassRepository schoolClassRepository, IUnitOfWork unitOfWork)
        : IDomainEventHandler<EnrollmentApprovedDomainEvent>
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly ISchoolClassRepository _schoolClassRepository = schoolClassRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(EnrollmentApprovedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var schoolClass = await _schoolClassRepository.GetClassById(domainEvent.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            var notification = Notification.Create(domainEvent.StudentId,
                $"Your enrollment in {schoolClass.Name.Value} class was approved by admin.");
            _notificationRepository.Add(notification);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
