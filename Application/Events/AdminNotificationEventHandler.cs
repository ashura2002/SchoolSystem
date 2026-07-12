using Application.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events
{
    public class AdminNotificationEventHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork,
        IUserRepository userRepository, ISchoolClassRepository schoolClassRepository)
        : IDomainEventHandler<EnrollmentRequestedDomainEvent>
    {

        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ISchoolClassRepository _schoolClassRepository = schoolClassRepository;

        public async Task Handle(EnrollmentRequestedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var admins = await _userRepository.GetAllAdminsAsync(cancellationToken);
            var schoolClass = await _schoolClassRepository.GetClassById(domainEvent.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            foreach (var admin in admins)
            {
                _notificationRepository.Add(Notification.Create(admin.Id,
                    $"A new enrollment request has been submitted for {schoolClass.Name.Value} class."));
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
