using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Enrollments.Student
{
    public class GetAllMyClassesUseCase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetAllMyClassesUseCase(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService)
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
        }


        public async Task<IEnumerable<EnrollmentDTO>> Execute(PaginationDTO pagination)
        {
            var studentId = _currentUserService.UserId;
            var classes = await _enrollmentRepository.GetApprovedByStudentId(pagination, studentId);
            return EnrollmentMapper.ToResponseList(classes);

        }

    }
}
