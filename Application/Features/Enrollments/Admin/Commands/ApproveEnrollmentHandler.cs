using Application.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Admin.Commands
{
    public class ApproveEnrollmentHandler:IRequestHandler<ApprovedEnrollmentCommand>
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

        public async Task Handle(ApprovedEnrollmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Approving enrollment {EnrollmentId}", request.EnrollmentId);

            var enrollmentToApprove = await _enrollmentRepository.GetEnrollmentByIdAsync(request.EnrollmentId, cancellationToken) ??
                throw new DomainNotFoundException("Enrollment not found");

            var schoolClass = await _schoolClassRepository.GetClassByIdAsync(enrollmentToApprove.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");
            enrollmentToApprove.Approve();
            schoolClass.EnrollStudent();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
