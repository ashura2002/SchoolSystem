using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Mapper;

namespace Application.Features.Enrollments.Student.Commands
{
    public class RequestEnrollmentHandler
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;

        public RequestEnrollmentHandler(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService)
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<EnrollmentDTO> Handle(RequestEnrollmentCommand command, CancellationToken cancellationToken)
        {
            var studentId = _currentUserService.UserId;

            var existing = await _enrollmentRepository.GetByStudentAndClass(studentId, command.ClassId, cancellationToken);
            if (existing != null) throw new DomainBadRequestException("You are already enrolled in this class");


            // create entity
            var enrollment = new Enrollment(studentId, command.ClassId);

            _enrollmentRepository.Add(enrollment);
            await _enrollmentRepository.SaveChangesAsync(cancellationToken);

            return EnrollmentMapper.ToDto(enrollment);
        }

    }
}
