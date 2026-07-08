using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Student.Commands
{
    public class CancelEnrollmentHandler
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public CancelEnrollmentHandler(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService,
             IUnitOfWork unitOfWork)
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<EnrollmentDTO> Handle(CancelEnrollmentCommand command, CancellationToken cancellationToken)
        {
            var enrollment = await _enrollmentRepository.GetById(command.EnrollementId, cancellationToken) ??
                throw new DomainNotFoundException("Enrollment not found");
            if (enrollment.StudentId != _currentUserService.UserId)
                throw new DomainBadRequestException("You can only cancel your own enrollment");


            enrollment.Cancel();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return EnrollmentMapper.ToDto(enrollment);
        }
    }
}