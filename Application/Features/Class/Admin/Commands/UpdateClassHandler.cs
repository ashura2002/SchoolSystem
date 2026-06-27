using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Commands
{
    public class UpdateClassHandler
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public UpdateClassHandler(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }


        public async Task<SchoolClassDTO> Handle(UpdateClassCommand command, CancellationToken cancellationToken)
        {
            var updatedClassName = ClassNameValueObject.Create(command.Name);

            var schoolClass = await _schoolClassRepository.GetClassById(command.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            schoolClass.UpdateClassName(updatedClassName);
            await _schoolClassRepository.SaveChangesAsync(cancellationToken);
            return SchoolClassMapper.ToDto(schoolClass);
        }

    }
}
