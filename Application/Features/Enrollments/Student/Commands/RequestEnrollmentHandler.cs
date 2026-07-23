using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using System;

namespace Application.Features.Enrollments.Student.Commands
{
    public class RequestEnrollmentHandler
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public RequestEnrollmentHandler(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(RequestEnrollmentCommand command, CancellationToken cancellationToken)
        {
            var studentId = _currentUserService.UserId;

            var existing = await _enrollmentRepository.GetEnrollmentByStudentAndClassAsync(studentId, command.ClassId, cancellationToken);
            if (existing != null) throw new DomainBadRequestException("You are already enrolled in this class");

            // create entity
            var enrollment = Enrollment.Request(studentId, command.ClassId);

            _enrollmentRepository.Add(enrollment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return enrollment.Id;
        }

    }
}
