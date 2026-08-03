using Application.DTOs;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Features.Enrollments.Student.Queries
{
    public class GetAllMyClassesHandler:IRequestHandler<GetAllMyClassesQuery, List<EnrollmentResponseDTO>>
    {
        private readonly IEnrollmentReadRespository _enrollmentReadRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISchoolClassReadRepository _schoolClassReadRepository;

        public GetAllMyClassesHandler(
            IEnrollmentReadRespository enrollmentRepository, 
            ICurrentUserService currentUserService,
            ISchoolClassReadRepository schoolClassReadRepository
            )
        {
            _enrollmentReadRepository = enrollmentRepository;
            _currentUserService = currentUserService;
            _schoolClassReadRepository = schoolClassReadRepository;
        }


        public async Task<List<EnrollmentResponseDTO>> Handle(GetAllMyClassesQuery request,
            CancellationToken cancellationToken)
        {
            var studentId = _currentUserService.UserId;

            var enrollments = await _enrollmentReadRepository.GetApprovedEnrollmentByStudentIdAsync(
                request.Page, 
                request.PageSize,
                studentId, 
                cancellationToken);

            //extract ids using select
            var classIds = enrollments.Select(sc => sc.ClassId).ToList();

            var classes = await _schoolClassReadRepository.GetClassesByIdsAsync(
                classIds, 
                cancellationToken);


            // Convert the list of classes into a dictionary:
            // Key   = ClassId
            // Value = ClassName
            var classLookUp = classes.ToDictionary(
                c => c.Id, 
                c => c.Name);

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
