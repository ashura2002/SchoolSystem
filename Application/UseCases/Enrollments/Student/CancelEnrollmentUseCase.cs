using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Enrollments.Student
{
    public class CancelEnrollmentUseCase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;

        public CancelEnrollmentUseCase(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService)
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<EnrollmentDTO> Execute(Guid enrollmentId,CancellationToken cancellationToken)
        {
            var enrollment = await _enrollmentRepository.GetById(enrollmentId,cancellationToken) ??
                throw new DomainNotFoundException("Enrollment not found");
            if (enrollment.StudentId != _currentUserService.UserId)
                throw new DomainBadRequestException("You can only cancel your own enrollment");


            enrollment.Cancel();
            await _enrollmentRepository.SaveChangesAsync(cancellationToken);
            return EnrollmentMapper.ToDto(enrollment);
        }
    }
}