using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
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

        public ApproveEnrollmentHandler(IEnrollmentRepository enrollmentRepository, ILogger<ApproveEnrollmentHandler> logger)
        {
            _enrollmentRepository = enrollmentRepository;
            _logger = logger;
        }

        public async Task<EnrollmentDTO> Handle(ApprovedEnrollmentCommand command,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Approving enrollment {EnrollmentId}", command.EnrollmentId);

            var enrollmentToApprove = await _enrollmentRepository.GetById(command.EnrollmentId,cancellationToken) ??
                throw new DomainNotFoundException("Enrollment not found");

            enrollmentToApprove.Approve();
            await _enrollmentRepository.SaveChangesAsync(cancellationToken);
            return EnrollmentMapper.ToDto(enrollmentToApprove);
        }

    }
}
