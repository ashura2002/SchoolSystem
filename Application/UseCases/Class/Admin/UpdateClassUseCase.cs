using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class.Admin
{
    public class UpdateClassUseCase
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public UpdateClassUseCase(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }


        public async Task<SchoolClassDTO> Execute(UpdateClassNameDTO Dto, Guid ClassId, CancellationToken cancellationToken)
        {
            var updatedClassName = ClassNameValueObject.Create(Dto.Name);

            var schoolClass = await _schoolClassRepository.GetClassById(ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            schoolClass.UpdateClassName(updatedClassName);
            await _schoolClassRepository.SaveChangesClassAsync(cancellationToken);
            return SchoolClassMapper.ToDto(schoolClass);
        }

    }
}
