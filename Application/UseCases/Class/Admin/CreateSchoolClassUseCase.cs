using Application.DTOs;
using Application.Interfaces;
using Domain.ValueObjects;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Mapper;

namespace Application.UseCases.Class.Admin
{
    public class CreateSchoolClassUseCase
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public CreateSchoolClassUseCase(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<SchoolClassDTO> Execute(CreateClassDTO dto, CancellationToken cancellationToken)
        {
            var schoolClasName = ClassNameValueObject.Create(dto.Name);
            var schoolClass = new SchoolClass(schoolClasName);

            await _schoolClassRepository.AddClass(schoolClass);
            await _schoolClassRepository.SaveChangesClassAsync(cancellationToken);
            return SchoolClassMapper.ToDto(schoolClass);

        }
    }
}
