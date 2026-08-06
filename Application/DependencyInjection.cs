using Application.Events;
using Application.Features.Auth.Services;
using Application.Features.Users.Services;
using Application.Interfaces;
using Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Get the compiled Application assembly (Application.dll)
            var assembly = typeof(DependencyInjection).Assembly;
            
            services.AddMediatR(cfg =>
            {
                // Scan the assembly and automatically register all IRequestHandler implementations
                cfg.RegisterServicesFromAssembly(assembly);
            });

            // application service
            services.AddScoped<UserRegistrationService>();
            services.AddScoped<GetByUsernameHandler>();
            services.AddScoped<GetUserByEmailHandler>();

            // register event
            services.AddScoped<IDomainEventHandler<EnrollmentRequestedDomainEvent>, StudentNotificationEventHandler>();
            services.AddScoped<IDomainEventHandler<EnrollmentRequestedDomainEvent>, AdminNotificationEventHandler>();
            services.AddScoped<IDomainEventHandler<EnrollmentApprovedDomainEvent>, ApprovedEnrollmentEventHandler>();
            services.AddScoped<IDomainEventHandler<EnrollmentRejectedDomainEvent>, RejectEnrollmentEventHandler>();
            services.AddScoped<IDomainEventHandler<TeacherAssignedToClassDomainEvent>, TeacherAssignedToClassDomainEventHandler>();
            services.AddScoped<IDomainEventHandler<TeacherRemovedFromClassDomainEvent>, TeacherRemovedFromClassDomainEventHandler>();
            return services;
        }

    }
}
