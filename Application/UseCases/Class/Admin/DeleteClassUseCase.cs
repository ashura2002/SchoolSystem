using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class.Admin
{
    public class DeleteClassUseCase
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public DeleteClassUseCase(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }


        public async Task Execute(Guid classId,CancellationToken cancellationToken)
        {
            // load data from db
            var schoolClass = await _schoolClassRepository.GetClassById(classId,cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            // tell ef core mark as deleted
            _schoolClassRepository.DeleteClass(schoolClass);
            // persist changes
            await _schoolClassRepository.SaveChangesClassAsync(cancellationToken);
        }

    }
}
