using Application.Interfaces;
using Domain.ValueObjects;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Features.Class.Admin.Commands
{
    public class CreateSchoolClassHandler:IRequestHandler<CreateSchoolClassCommand, Guid>
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSchoolClassHandler
            (ISchoolClassRepository schoolClassRepository, 
            IUnitOfWork unitOfWork)
        {
            _schoolClassRepository = schoolClassRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateSchoolClassCommand request, CancellationToken cancellationToken)
        {
            var schoolClassName = ClassNameVO.Create(request.Name);
            var schoolClass = SchoolClass.Create(schoolClassName, request.StartTime, request.EndTime, request.Schedule,
                request.StudentCapacity);

            _schoolClassRepository.Add(schoolClass);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return schoolClass.Id;
        }
    }
}
