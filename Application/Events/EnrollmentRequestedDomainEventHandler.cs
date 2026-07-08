using Application.Interfaces;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events
{
    public class EnrollmentRequestedDomainEventHandler(IEmailService emailService)
        : IDomainEventHandler<EnrollementRequestedDomainEvent>
    {
        private readonly IEmailService _email = emailService;

        public async Task Handle(EnrollementRequestedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            Console.WriteLine("EVENT HANDLER: EnrollmentRequestedDomainEventHandler");
            await _email.SendEmail(domainEvent.EnrollmentId, domainEvent.StudentId, domainEvent.ClassId);
        }
    }
}
