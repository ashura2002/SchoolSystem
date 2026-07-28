using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Admin.Commands
{
    public class RejectEnrollmentHandler : IRequestHandler<RejectEnrollmentCommand>
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

        public async Task Handle(RejectEnrollmentCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Reject enrollment for {EnrollmentId}", request.EnrollmentId);

            var requestToReject = await _enrollmentRepository.GetEnrollmentByIdAsync(request.EnrollmentId, cancellationToken) ??
                throw new DomainNotFoundException("Enrollment not found");

            requestToReject.Reject();
        }
    }
}
