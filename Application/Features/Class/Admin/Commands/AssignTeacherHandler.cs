using Application.Interfaces;
using Domain.Exceptions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Application.Features.Class.Admin.Commands
{
    public class AssignTeacherHandler
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AssignTeacherHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AssignTeacherHandler(ISchoolClassRepository schoolClassRepository, IUserRepository userRepository,
            ILogger<AssignTeacherHandler> logger, IUnitOfWork unitOfWork
            )
        {
            _schoolClassRepository = schoolClassRepository;
            _userRepository = userRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AssignTeacherCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assigning teacher {TeacherId} to class {ClassId}", command.TeacherId, command.ClassId);

            var schoolClass = await _schoolClassRepository.GetClassByIdAsync(command.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            var teacher = await _userRepository.GetByIdAsync(command.TeacherId, cancellationToken) ??
                throw new DomainNotFoundException("Teacher not found");

            if (teacher.Role != Role.Teacher)
                throw new DomainBadRequestException("This user is not a teacher.");


            schoolClass.AssignTeacher(command.TeacherId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }


    }
}
