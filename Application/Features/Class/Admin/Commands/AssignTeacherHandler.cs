using Application.Interfaces;
using Domain.Exceptions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Application.Features.Class.Admin.Commands
{
    public class AssignTeacherHandler:IRequestHandler<AssignTeacherCommand>
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ISchoolClassReadRepository _schoolClassReadRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AssignTeacherHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AssignTeacherHandler(
            ISchoolClassRepository schoolClassRepository,
            ISchoolClassReadRepository schoolClassReadRepository,
            IUserRepository userRepository,
            ILogger<AssignTeacherHandler> logger, IUnitOfWork unitOfWork
            )
        {
            _schoolClassRepository = schoolClassRepository;
            _schoolClassReadRepository = schoolClassReadRepository;
            _userRepository = userRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AssignTeacherCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assigning teacher {TeacherId} to class {ClassId}", request.TeacherId, request.ClassId);

            var schoolClass = await _schoolClassRepository.GetClassByIdAsync(request.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            var user = await _userRepository.GetByIdAsync(request.TeacherId, cancellationToken) ??
                throw new DomainNotFoundException("Teacher not found");

            if (user.Role != Role.Teacher)
                throw new DomainBadRequestException("This user is not a teacher.");


            var isAvailable = await _schoolClassReadRepository.IsTeacherAvailableAsync(
                    request.TeacherId,
                    schoolClass.Schedule,
                    schoolClass.StartTime,
                    schoolClass.EndTime,
                    cancellationToken
                );

            if (isAvailable)
                throw new DomainBadRequestException(
                    "Teacher already has another class at this schedule.");

            schoolClass.AssignTeacher(request.TeacherId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
