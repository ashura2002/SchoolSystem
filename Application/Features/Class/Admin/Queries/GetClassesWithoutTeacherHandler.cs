using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Queries
{
    public class GetClassesWithoutTeacherHandler
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetClassesWithoutTeacherHandler(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<IEnumerable<SchoolClassDTO>> Handle(GetClassesWithoutTeacherQuery query,
            CancellationToken cancellationToken)
        {
            var schoolClasses = await _schoolClassRepository.GetAllClassesWithoutTeacher(query.Page, query.PageSize,
                cancellationToken);
            return SchoolClassMapper.ToResponseList(schoolClasses);
        }
    }
}
