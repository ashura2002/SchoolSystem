using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Enrollments.Admin
{
    public class ApproveEnrollmentUseCase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ILogger<ApproveEnrollmentUseCase> _logger;

        public ApproveEnrollmentUseCase(IEnrollmentRepository enrollmentRepository, ILogger<ApproveEnrollmentUseCase> logger)
        {
            _enrollmentRepository = enrollmentRepository;
            _logger = logger;
        }

        public async Task<EnrollmentDTO> Execute(Guid enrollmentId,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Approving enrollment {enrollmentId}", enrollmentId);

            var enrollmentToApprove = await _enrollmentRepository.GetById(enrollmentId,cancellationToken) ??
                throw new DomainNotFoundException("Enrollment not found");

            enrollmentToApprove.Approve();
            await _enrollmentRepository.SaveChangesAsync(cancellationToken);
            return EnrollmentMapper.ToDto(enrollmentToApprove);
        }

    }
}
