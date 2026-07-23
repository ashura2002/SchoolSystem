using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Admin.Commands
{
    public class ApproveEnrollmentHandler
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ILogger<ApproveEnrollmentHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISchoolClassRepository _schoolClassRepository;

        public ApproveEnrollmentHandler(IEnrollmentRepository enrollmentRepository, ILogger<ApproveEnrollmentHandler> logger,
            IUnitOfWork unitOfWork, ISchoolClassRepository schoolClass
            )
        {
            _enrollmentRepository = enrollmentRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _schoolClassRepository = schoolClass;
        }

        public async Task Handle(ApprovedEnrollmentCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Approving enrollment {EnrollmentId}", command.EnrollmentId);

            var enrollmentToApprove = await _enrollmentRepository.GetEnrollmentByIdAsync(command.EnrollmentId, cancellationToken) ??
                throw new DomainNotFoundException("Enrollment not found");

            var schoolClass = await _schoolClassRepository.GetClassByIdAsync(enrollmentToApprove.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");
            enrollmentToApprove.Approve();
            schoolClass.EnrollStudent();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
