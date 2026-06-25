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
    public class RejectEnrollmentUseCase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ILogger<RejectEnrollmentUseCase> _logger;

        public RejectEnrollmentUseCase(IEnrollmentRepository enrollmentRepository, ILogger<RejectEnrollmentUseCase> logger)
        {
            _enrollmentRepository = enrollmentRepository;
            _logger = logger;
        }


        public async Task<EnrollmentDTO> Execute(Guid classId,CancellationToken cancellationToken)
        {

            _logger.LogInformation("Reject enrollment for {classId}", classId);

            var requestToReject = await _enrollmentRepository.GetById(classId,cancellationToken) ??
                throw new DomainNotFoundException("Enrollment not found");

            requestToReject.Reject();
            await _enrollmentRepository.SaveChangesAsync(cancellationToken);
            return EnrollmentMapper.ToDto(requestToReject);

        }

    }
}
