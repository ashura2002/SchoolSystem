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
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetAllMyClassesHandler(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService,
            ISchoolClassRepository schoolClassRepository
            )
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
            _schoolClassRepository = schoolClassRepository;
        }


        public async Task<IEnumerable<EnrollmentResponseDTO>> Handle(GetAllMyClassesQuery query,
            CancellationToken cancellationToken)
        {
            var studentId = _currentUserService.UserId;
            var enrollments = await _enrollmentRepository.GetApprovedByStudentId(query.Page, query.PageSize,
                studentId, cancellationToken);

            var classIds = enrollments.Select(sc => sc.ClassId).ToList();


            var classes = await _schoolClassRepository.GetClassesByIds(classIds, cancellationToken);

            var classLookUp = classes.ToDictionary(c => c.Id, c => c.Name.Value);


            return enrollments.Select(e => new EnrollmentResponseDTO(
                e.Id,
                e.Status,
                classLookUp[e.ClassId],
                e.CreatedAt,
                e.UpdatedAt,
                e.DeletedAt));
        }

    }
}
