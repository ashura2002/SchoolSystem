using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Commands
{
    public class DeleteClassHandler
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClassHandler(ISchoolClassRepository schoolClassRepository,IUnitOfWork unitOfWork)
        {
            _schoolClassRepository = schoolClassRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task Handle(DeleteClassCommand command, CancellationToken cancellationToken)
        {
            // load data from db
            var schoolClass = await _schoolClassRepository.GetClassById(command.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            schoolClass.EnsureCanBeDeleted();

            // tell ef core mark as deleted
            _schoolClassRepository.Remove(schoolClass);
            // persist changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
