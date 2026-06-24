using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class.Admin
{
    public class RemoveTeacherUseCase
    {
        private readonly ISchoolClassRepository _schoolClassRepository;


        public RemoveTeacherUseCase(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task Execute(Guid classId)
        {
            var schoolClass = await _schoolClassRepository.GetClassById(classId) ??
                throw new DomainNotFoundException("Class not found!");
            schoolClass.RemoveTeacher();
            await _schoolClassRepository.SaveChangesClassAsync();
        }

    }
}
