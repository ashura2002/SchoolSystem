using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class.Admin
{
    public class GetAllClassesWithTeacher
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetAllClassesWithTeacher(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<IEnumerable<SchoolClassDTO>> Execute(PaginationDTO pagination)
        {
            var schoolClasses = await _schoolClassRepository.GetAllClassesWithTeacher(pagination);
            return SchoolClassMapper.ToResponseList(schoolClasses);
        }
    }
}
