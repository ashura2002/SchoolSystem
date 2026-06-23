using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class.Teacher
{
    public class GetTeacherClassByIdUseCase
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ICurrentUserService _currentUserService;


        public GetTeacherClassByIdUseCase(ISchoolClassRepository schoolClassRepository, ICurrentUserService currentUserService)
        {
            _schoolClassRepository = schoolClassRepository;
            _currentUserService = currentUserService;
        }

        public async Task<SchoolClassDTO> Execute(Guid classId)
        {
            var schoolClass = await _schoolClassRepository.GetClassById(classId) ??
                throw new DomainNotFoundException("Class not found");

            if (schoolClass.TeacherId != _currentUserService.UserId)
                throw new DomainUnauthorizedException("You are not assigned to this class");

            return SchoolClassMapper.ToDto(schoolClass);
        }

    }
}
