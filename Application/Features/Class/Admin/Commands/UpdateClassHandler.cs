using Application.Interfaces;
using Domain.Exceptions;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Commands
{
    public class UpdateClassHandler:IRequestHandler<UpdateClassCommand>
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClassHandler(ISchoolClassRepository schoolClassRepository, IUnitOfWork unitOfWork)
        {
            _schoolClassRepository = schoolClassRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var updatedClassName = ClassNameVO.Create(request.Name);

            var schoolClass = await _schoolClassRepository.GetClassByIdAsync(request.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            schoolClass.UpdateClassName(updatedClassName);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
