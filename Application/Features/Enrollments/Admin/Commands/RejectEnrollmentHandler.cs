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
    public class RejectEnrollmentHandler
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ILogger<RejectEnrollmentHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public RejectEnrollmentHandler(IEnrollmentRepository enrollmentRepository, ILogger<RejectEnrollmentHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _enrollmentRepository = enrollmentRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        public async Task<EnrollmentDTO> Handle(RejectEnrollmentCommand command, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Reject enrollment for {EnrollmentId}", command.EnrollmentId);

            var requestToReject = await _enrollmentRepository.GetById(command.EnrollmentId, cancellationToken) ??
                throw new DomainNotFoundException("Enrollment not found");

            requestToReject.Reject();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return EnrollmentMapper.ToDto(requestToReject);

        }

    }
}
