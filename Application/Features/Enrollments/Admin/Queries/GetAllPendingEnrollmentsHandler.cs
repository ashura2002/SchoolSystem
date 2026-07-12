using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Features.Enrollments.Admin.Queries
{
    public class GetAllPendingEnrollmentsHandler
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IUserRepository _userRepository;

        public GetAllPendingEnrollmentsHandler(IEnrollmentRepository enrollmentRepository,
            ISchoolClassRepository schoolClassRepository, IUserRepository userRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _schoolClassRepository = schoolClassRepository;
            _userRepository = userRepository;
        }


        public async Task<List<PendingEnrollmentResponseDTO>> Handle(GetAllPendingEnrollmentQuery query,
            CancellationToken cancellationToken)
        {
            // enrollments
            var pendingEnrollments = await _enrollmentRepository.GetAllPendingEnrollmentsAsync(query.Page, query.PageSize,
                cancellationToken);

            // map the ids - use distinct to prevent passing duplicate ids
            var classIds = pendingEnrollments.Select(e => e.ClassId).Distinct();
            var usersIds = pendingEnrollments.Select(e => e.StudentId).Distinct();

            // get classes by ids
            var classes = await _schoolClassRepository.GetClassesByIds(classIds, cancellationToken);
            // get users by ids
            var students = await _userRepository.GetUsersByIdsAsync(usersIds, cancellationToken);

            // dictionary for class
            var classLookUp = classes.ToDictionary(c => c.Id, c => c.Name.Value);
            // look up for users
            var userLookUp = students.ToDictionary(u => u.Id, u => u.Username.Value);

            var result = pendingEnrollments.Select(e => new PendingEnrollmentResponseDTO(
                e.Id,
                e.Status,
                userLookUp.GetValueOrDefault(e.StudentId, "Unknown"),
                classLookUp.GetValueOrDefault(e.ClassId, "Unknown"),
                e.CreatedAt,
                e.UpdatedAt,
                e.DeletedAt)).ToList();

            return result;
        }

    }
}
