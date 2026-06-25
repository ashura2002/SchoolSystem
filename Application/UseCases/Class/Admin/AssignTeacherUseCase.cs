using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Class.Admin
{
    public class AssignTeacherUseCase
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AssignTeacherUseCase> _logger;


        public AssignTeacherUseCase(ISchoolClassRepository schoolClassRepository, IUserRepository userRepository,
            ILogger<AssignTeacherUseCase> logger
            )
        {
            _schoolClassRepository = schoolClassRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<SchoolClassDTO> Execute(AssignTeacherDTO dto, Guid classId,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assigning class to teacher id {Id}", dto.Id);

            var schoolClass = await _schoolClassRepository.GetClassById(classId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            var teacher = await _userRepository.GetById(dto.Id,cancellationToken) ??
                throw new DomainNotFoundException("Teacher not found");

            if (teacher.Role != Role.Teacher)
                throw new DomainBadRequestException("This user is not a teacher.");


            schoolClass.AssignTeacher(dto.Id);

            await _schoolClassRepository.SaveChangesClassAsync(cancellationToken);
            return SchoolClassMapper.ToDto(schoolClass);
        }


    }
}
