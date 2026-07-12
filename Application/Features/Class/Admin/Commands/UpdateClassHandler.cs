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
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClassHandler(ISchoolClassRepository schoolClassRepository, IUnitOfWork unitOfWork)
        {
            _schoolClassRepository = schoolClassRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task Handle(UpdateClassCommand command, CancellationToken cancellationToken)
        {
            var updatedClassName = ClassNameValueObject.Create(command.Name);

            var schoolClass = await _schoolClassRepository.GetClassById(command.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            schoolClass.UpdateClassName(updatedClassName);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
