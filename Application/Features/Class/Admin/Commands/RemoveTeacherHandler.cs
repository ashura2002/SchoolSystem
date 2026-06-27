using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Commands
{
    public class RemoveTeacherHandler
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ILogger<RemoveTeacherHandler> _logger;


        public RemoveTeacherHandler(ISchoolClassRepository schoolClassRepository, ILogger<RemoveTeacherHandler> logger)
        {
            _schoolClassRepository = schoolClassRepository;
            _logger = logger;
        }

        public async Task Handle(RemoveTeacherCommand command,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Removing teacher from class id {classId}", command.ClassId);

            var schoolClass = await _schoolClassRepository.GetClassById(command.ClassId,cancellationToken) ??
                throw new DomainNotFoundException("Class not found!");
            schoolClass.RemoveTeacher();
            await _schoolClassRepository.SaveChangesAsync(cancellationToken);
        }

    }
}
