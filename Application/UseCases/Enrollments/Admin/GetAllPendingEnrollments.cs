using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Enrollments.Admin
{
    public class GetAllPendingEnrollments
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public GetAllPendingEnrollments(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }


        public async Task<IEnumerable<EnrollmentDTO>> Execute(PaginationDTO pagination,CancellationToken cancellationToken)
        {
            var pendingEnrollments = await _enrollmentRepository.GetAllPendingEnrollments(pagination, cancellationToken);
            return EnrollmentMapper.ToResponseList(pendingEnrollments);
        }

    }
}
