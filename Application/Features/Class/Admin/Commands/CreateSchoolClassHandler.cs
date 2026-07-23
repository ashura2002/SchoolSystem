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
        private readonly IUnitOfWork _unitOfWork;

        public CreateSchoolClassHandler(ISchoolClassRepository schoolClassRepository, IUnitOfWork unitOfWork)
        {
            _schoolClassRepository = schoolClassRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateSchoolClassCommand command, CancellationToken cancellationToken)
        {
            var schoolClassName = ClassNameValueObject.Create(command.Name);
            var schoolClass = SchoolClass.Create(schoolClassName, command.StartTime, command.EndTime, command.Schedule,
                command.StudentCapacity);

            _schoolClassRepository.Add(schoolClass);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return schoolClass.Id;

        }
    }
}
