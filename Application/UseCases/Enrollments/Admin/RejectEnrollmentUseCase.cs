using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Enrollments.Admin
{
    public class RejectEnrollmentUseCase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public RejectEnrollmentUseCase(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }


        public async Task<EnrollmentDTO> Execute(Guid classId)
        {
            var requestToReject = await _enrollmentRepository.GetById(classId) ??
                throw new DomainNotFoundException("Enrollment not found");

            requestToReject.Reject();
            await _enrollmentRepository.SaveChangesAsync();
            return EnrollmentMapper.ToDto(requestToReject);

        }

    }
}
