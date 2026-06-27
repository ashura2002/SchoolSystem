using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Admin.Queries
{
    public class GetAllPendingEnrollmentsHandler
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public GetAllPendingEnrollmentsHandler(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }


        public async Task<IEnumerable<EnrollmentDTO>> Execute(GetAllPendingEnrollmentQuery query,
            CancellationToken cancellationToken)
        {
            var pendingEnrollments = await _enrollmentRepository.GetAllPendingEnrollments(query.Page, query.PageSize,
                cancellationToken);
            return EnrollmentMapper.ToResponseList(pendingEnrollments);
        }

    }
}
