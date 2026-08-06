using Application.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events
{
    public class TeacherRemovedFromClassDomainEventHandler : IDomainEventHandler<TeacherRemovedFromClassDomainEvent>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeacherRemovedFromClassDomainEventHandler(
             INotificationRepository notificationRepository,
             IUserRepository userRepository,
             IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TeacherRemovedFromClassDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var teacher = await _userRepository.GetByIdAsync(domainEvent.TeacherId,cancellationToken)?? 
                throw new DomainNotFoundException("User not found.");

            var notification = Notification.Create(
                teacher.Id,
                $"You are no longer assigned to teach the {domainEvent.ClassName} class.");

            _notificationRepository.Add(notification);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
