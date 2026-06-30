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
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IUserRepository _userRepository;

        public GetAllPendingEnrollmentsHandler(IEnrollmentRepository enrollmentRepository,
            ISchoolClassRepository schoolClassRepository, IUserRepository userRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _schoolClassRepository = schoolClassRepository;
            _userRepository = userRepository;
        }


        public async Task<IEnumerable<PendingEnrollmentResponseDTO>> Execute(GetAllPendingEnrollmentQuery query,
            CancellationToken cancellationToken)
        {
            // enrollments
            var pendingEnrollments = await _enrollmentRepository.GetAllPendingEnrollments(query.Page, query.PageSize,
                cancellationToken);

            // map the ids 
            var classIds = pendingEnrollments.Select(e => e.ClassId).ToList();
            var usersIds = pendingEnrollments.Select(e => e.StudentId).ToList();

            // get classes by ids
            var classes = await _schoolClassRepository.GetClassesByIds(classIds, cancellationToken);
            // get users by ids
            var students = await _userRepository.GetUsersByIds(usersIds, cancellationToken);

            // dictionary for class
            var classLookUp = classes.ToDictionary(c => c.Id, c => c.Name.Value);
            // look up for users
            var userLookUp = students.ToDictionary(u => u.Id, u => u.Username.Value);


            return pendingEnrollments.Select(e => new PendingEnrollmentResponseDTO(
                e.Id,
                e.Status,
                userLookUp[e.StudentId],
                classLookUp[e.ClassId],
                e.CreatedAt,
                e.UpdatedAt,
                e.DeletedAt));

        }

    }
}
