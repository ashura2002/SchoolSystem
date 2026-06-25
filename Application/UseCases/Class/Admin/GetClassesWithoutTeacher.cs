using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class.Admin
{
    public class GetClassesWithoutTeacher
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetClassesWithoutTeacher(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<IEnumerable<SchoolClassDTO>> Execute(PaginationDTO pagination, CancellationToken cancellationToken)
        {
            var schoolClasses = await _schoolClassRepository.GetAllClassesWithoutTeacher(pagination, cancellationToken);
            return SchoolClassMapper.ToResponseList(schoolClasses);
        }
    }
}
