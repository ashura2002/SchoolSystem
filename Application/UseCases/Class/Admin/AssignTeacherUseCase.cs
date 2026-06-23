using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class.Admin
{
    public class AssignTeacherUseCase
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IUserRepository _userRepository;


        public AssignTeacherUseCase(ISchoolClassRepository schoolClassRepository, IUserRepository userRepository)
        {
            _schoolClassRepository = schoolClassRepository;
            _userRepository = userRepository;
        }

        public async Task<SchoolClassDTO> Execute(AssignTeacherDTO dto, Guid classId)
        {

            var schoolClass = await _schoolClassRepository.GetClassById(classId) ??
                throw new DomainNotFoundException("Class not found");

            var teacher = await _userRepository.GetById(dto.Id) ??
                throw new DomainNotFoundException("Teacher not found");

            if (teacher.Role != Role.Teacher)
                throw new DomainBadRequestException("This user is not a teacher.");


            schoolClass.AssignTeacher(dto.Id);

            await _schoolClassRepository.SaveChangesClassAsync();
            return SchoolClassMapper.ToDto(schoolClass);
        }


    }
}
