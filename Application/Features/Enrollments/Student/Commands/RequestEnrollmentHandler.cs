using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System;

namespace Application.Features.Enrollments.Student.Commands
{
    public class RequestEnrollmentHandler : IRequestHandler<RequestEnrollmentCommand, Guid>
    {
        private readonly IEnrollmentReadRespository _enrollmentReadRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public RequestEnrollmentHandler(
            IEnrollmentReadRespository enrollmentReadRepository, 
            IEnrollmentRepository enrollmentRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _enrollmentReadRepository = enrollmentReadRepository;
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(RequestEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var studentId = _currentUserService.UserId;

            if (await _enrollmentReadRepository.EnrollmentExistsAsync(
                    studentId,
                    request.ClassId,
                    cancellationToken))
            {
                throw new DomainBadRequestException(
                    "You are already enrolled in this class");
            }

            // create entity
            var enrollment = Enrollment.Request(studentId, request.ClassId);

            _enrollmentRepository.Add(enrollment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return enrollment.Id; ;
        }

    }
}
