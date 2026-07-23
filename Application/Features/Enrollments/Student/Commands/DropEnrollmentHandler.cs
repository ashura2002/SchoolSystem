using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Student.Commands
{
    public class DropEnrollmentHandler
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISchoolClassRepository _schoolClassRepository;

        public DropEnrollmentHandler(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService,
             IUnitOfWork unitOfWork, ISchoolClassRepository schoolClass)
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _schoolClassRepository = schoolClass;
        }

        public async Task Handle(DropEnrollmentCommand command, CancellationToken cancellationToken)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(command.EnrollmentId, cancellationToken) ??
           throw new DomainNotFoundException("Enrollment not found");
            if (enrollment.StudentId != _currentUserService.UserId)
                throw new DomainBadRequestException("You can only drop your own enrollment");

            var schoolClass = await _schoolClassRepository.GetClassByIdAsync(enrollment.ClassId, cancellationToken) ??
               throw new DomainNotFoundException("Class not found");
            enrollment.Drop();
            schoolClass.RemoveStudent();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
