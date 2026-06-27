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

        public DeleteClassHandler(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }


        public async Task Handle(DeleteClassCommand command, CancellationToken cancellationToken)
        {
            // load data from db
            var schoolClass = await _schoolClassRepository.GetClassById(command.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            schoolClass.EnsureCanBeDeleted();

            // tell ef core mark as deleted
            _schoolClassRepository.DeleteClass(schoolClass);
            // persist changes
            await _schoolClassRepository.SaveChangesAsync(cancellationToken);
        }

    }
}
