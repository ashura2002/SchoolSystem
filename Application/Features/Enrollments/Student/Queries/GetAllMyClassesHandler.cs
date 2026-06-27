using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Student.Queries
{
    public class GetAllMyClassesHandler
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetAllMyClassesHandler(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService)
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
        }


        public async Task<IEnumerable<EnrollmentDTO>> Handle(GetAllMyClassesQuery query, CancellationToken cancellationToken)
        {
            var studentId = _currentUserService.UserId;
            var classes = await _enrollmentRepository.GetApprovedByStudentId(query.Page, query.PageSize, studentId, cancellationToken);
            return EnrollmentMapper.ToResponseList(classes);

        }

    }
}
