using Application.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Commands
{
    public class RemoveTeacherHandler : IRequestHandler<RemoveTeacherCommand>
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ILogger<RemoveTeacherHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public RemoveTeacherHandler(ISchoolClassRepository schoolClassRepository, ILogger<RemoveTeacherHandler> logger,
             IUnitOfWork unitOfWork)
        {
            _schoolClassRepository = schoolClassRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveTeacherCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Removing teacher from class id {classId}", request.ClassId);

            var schoolClass = await _schoolClassRepository.GetClassByIdAsync(request.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found!");
            schoolClass.RemoveTeacher();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
