using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Student.Commands
{
    public class DropEnrollmentHandler
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public DropEnrollmentHandler(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService,
             IUnitOfWork unitOfWork)
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<EnrollmentDTO> Handle(DropEnrollmentCommand command, CancellationToken cancellationToken)
        {
            var enrollment = await _enrollmentRepository.GetById(command.EnrollmentId, cancellationToken) ??
                throw new DomainNotFoundException("Enrollment not found");
            if (enrollment.StudentId != _currentUserService.UserId)
                throw new DomainBadRequestException("You can only drop your own enrollment");

            enrollment.Drop();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return EnrollmentMapper.ToDto(enrollment);
        }

    }
}
