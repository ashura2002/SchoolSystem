using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Enrollments.Admin
{
    public class ApproveEnrollmentUseCase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public ApproveEnrollmentUseCase(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<EnrollmentDTO> Execute(Guid enrollmentId)
        {
            var enrollmentToApprove = await _enrollmentRepository.GetById(enrollmentId) ??
                throw new DomainNotFoundException("Enrollment not found");

            enrollmentToApprove.Approve();
            await _enrollmentRepository.SaveChangesAsync();
            return EnrollmentMapper.ToDto(enrollmentToApprove);
        }

    }
}
