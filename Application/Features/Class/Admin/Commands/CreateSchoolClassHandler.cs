using Application.DTOs;
using Application.Interfaces;
using Domain.ValueObjects;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Mapper;

namespace Application.Features.Class.Admin.Commands
{
    public class CreateSchoolClassHandler
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public CreateSchoolClassHandler(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<SchoolClassDTO> Handle(CreateSchoolClassCommand command, CancellationToken cancellationToken)
        {
            var schoolClassName = ClassNameValueObject.Create(command.Name);
            var schoolClass = new SchoolClass(schoolClassName);

            await _schoolClassRepository.AddClass(schoolClass);
            await _schoolClassRepository.SaveChangesAsync(cancellationToken);
            return SchoolClassMapper.ToDto(schoolClass);

        }
    }
}
