using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;

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


        public async Task<List<EnrollmentResponseDTO>> Handle(GetAllMyClassesQuery query,
            CancellationToken cancellationToken)
        {
            var studentId = _currentUserService.UserId;
            var enrollments = await _enrollmentRepository.GetApprovedByStudentIdAsync(query.Page, query.PageSize,
                studentId, cancellationToken);

            var classIds = enrollments.Select(sc => sc.ClassId).ToList();


            var classes = await _schoolClassRepository.GetClassesByIds(classIds, cancellationToken);

            var classLookUp = classes.ToDictionary(c => c.Id, c => c.Name.Value);


            var result = enrollments.Select(e => new EnrollmentResponseDTO(
                e.Id,
                e.Status,
                classLookUp.GetValueOrDefault(e.ClassId, "Unknown"),
                e.CreatedAt,
                e.UpdatedAt,
                e.DeletedAt)).ToList();

            return result;
        }

    }
}
