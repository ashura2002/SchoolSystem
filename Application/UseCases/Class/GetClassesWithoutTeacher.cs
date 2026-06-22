using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class
{
    public class GetClassesWithoutTeacher
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetClassesWithoutTeacher(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<IEnumerable<SchoolClassDTO>> Execute(PaginationDTO pagination)
        {
            var schoolClasses = await _schoolClassRepository.GetAllClassesWithoutTeacher(pagination);
            return SchoolClassMapper.ToResponseList(schoolClasses);
        }
    }
}
