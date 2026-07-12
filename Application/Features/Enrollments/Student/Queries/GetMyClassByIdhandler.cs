using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Student.Queries
{
    public class GetMyClassByIdhandler
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetMyClassByIdhandler(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService,
            ISchoolClassRepository schoolClassRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
            _schoolClassRepository = schoolClassRepository;
        }


        public async Task<EnrollmentDetailsDTO> Handle(GetMyClassByIdQuery query, CancellationToken cancellationToken)
        {
            var studentId = _currentUserService.UserId;
            var enrollment = await _enrollmentRepository.GetByIdAsync(query.EnrollmentId, cancellationToken) ??
                throw new DomainNotFoundException("Enrollment not found");
            if (enrollment.StudentId != studentId) throw new DomainBadRequestException("You can only see your own enrollment");

            var schoolClass = await _schoolClassRepository.GetClassById(enrollment.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            return new EnrollmentDetailsDTO(enrollment.Id, schoolClass.Name.Value, enrollment.Status);
        }

    }
}
