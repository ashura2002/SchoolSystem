using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class.Admin
{
    public class RemoveTeacherUseCase
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ILogger<RemoveTeacherUseCase> _logger;


        public RemoveTeacherUseCase(ISchoolClassRepository schoolClassRepository, ILogger<RemoveTeacherUseCase> logger)
        {
            _schoolClassRepository = schoolClassRepository;
            _logger = logger;
        }

        public async Task Execute(Guid classId,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Removing teacher from class id {classId}", classId);

            var schoolClass = await _schoolClassRepository.GetClassById(classId,cancellationToken) ??
                throw new DomainNotFoundException("Class not found!");
            schoolClass.RemoveTeacher();
            await _schoolClassRepository.SaveChangesClassAsync(cancellationToken);
        }

    }
}
