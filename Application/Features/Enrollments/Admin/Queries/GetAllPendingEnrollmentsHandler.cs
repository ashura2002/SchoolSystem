using Application.DTOs;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Features.Enrollments.Admin.Queries
{
    public class GetAllPendingEnrollmentsHandler : IRequestHandler<GetAllPendingEnrollmentQuery, List<PendingEnrollmentResponseDTO>>
    {
        private readonly IEnrollmentReadRespository _enrollmentReadRepository;
        private readonly ISchoolClassReadRepository _schoolClassReadRepository;
        private readonly IUserReadRepository _userReadRepository;

        public GetAllPendingEnrollmentsHandler(
            IEnrollmentReadRespository enrollmentRepository,
            ISchoolClassReadRepository schoolClassRepository, 
            IUserReadRepository userReadRepository)
        {
            _enrollmentReadRepository = enrollmentRepository;
            _schoolClassReadRepository = schoolClassRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<List<PendingEnrollmentResponseDTO>> Handle(GetAllPendingEnrollmentQuery request, CancellationToken cancellationToken)
        {
             // enrollments
            var pendingEnrollments = await _enrollmentReadRepository.GetAllPendingEnrollmentsAsync(request.Page, request.PageSize,
                cancellationToken);

            // map the ids - use distinct to prevent passing duplicate ids
            var classIds = pendingEnrollments.Select(e => e.ClassId).Distinct();
            var usersIds = pendingEnrollments.Select(e => e.StudentId).Distinct();

           // get classes by ids
            var classes = await _schoolClassReadRepository.GetClassesByIdsAsync(classIds, cancellationToken);
             // get users by ids
            var students = await _userReadRepository.GetUsersByIdsAsync(usersIds, cancellationToken);

              // dictionary for class
            var classLookUp = classes.ToDictionary(c => c.Id, c => c.Name);
           // look up for users
            var userLookUp = students.ToDictionary(u => u.Id, u => u.Username);

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
