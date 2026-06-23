using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class.Admin
{
    public class GetClassByIdUseCase
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetClassByIdUseCase(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<SchoolClassDTO> Execute(Guid classId)
        {
            var schoolClass = await _schoolClassRepository.GetClassById(classId) ??
                throw new DomainNotFoundException("Class not found");
            return SchoolClassMapper.ToDto(schoolClass);
        }

    }
}
