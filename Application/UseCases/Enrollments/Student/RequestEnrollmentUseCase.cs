using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Mapper;

namespace Application.UseCases.Enrollments.Student
{
    public class RequestEnrollmentUseCase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;

        public RequestEnrollmentUseCase(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService)
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<EnrollmentDTO> Execute(CreateEnrollmentDTO dto)
        {
            var studentId = _currentUserService.UserId;

            var existing = await _enrollmentRepository.GetByStudentAndClass(studentId, dto.ClassId);
            if (existing != null) throw new DomainBadRequestException("You are already enrolled in this class");


            // create entity
            var enrollment = new Enrollment(studentId, dto.ClassId);

            _enrollmentRepository.Add(enrollment);
            await _enrollmentRepository.SaveChangesAsync();

            return EnrollmentMapper.ToDto(enrollment);
        }

    }
}
