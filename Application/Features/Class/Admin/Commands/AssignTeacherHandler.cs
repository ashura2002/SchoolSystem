using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
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


        public AssignTeacherHandler(ISchoolClassRepository schoolClassRepository, IUserRepository userRepository,
            ILogger<AssignTeacherHandler> logger
            )
        {
            _schoolClassRepository = schoolClassRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<SchoolClassDTO> Handle(AssignTeacherCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assigning teacher {TeacherId} to class {ClassId}", command.TeacherId, command.ClassId);

            var schoolClass = await _schoolClassRepository.GetClassById(command.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            var teacher = await _userRepository.GetById(command.TeacherId, cancellationToken) ??
                throw new DomainNotFoundException("Teacher not found");

            if (teacher.Role != Role.Teacher)
                throw new DomainBadRequestException("This user is not a teacher.");


            schoolClass.AssignTeacher(command.TeacherId);

            await _schoolClassRepository.SaveChangesAsync(cancellationToken);
            return SchoolClassMapper.ToDto(schoolClass);
        }


    }
}
