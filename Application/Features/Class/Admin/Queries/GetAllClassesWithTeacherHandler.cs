using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Queries
{
    public class GetAllClassesWithTeacherHandler
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetAllClassesWithTeacherHandler(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<List<SchoolClassDTO>> Handle(GetAllClassesWithTeacherQuery query,
            CancellationToken cancellationToken)
        {
            var schoolClasses = await _schoolClassRepository.GetAllClassesWithTeacherAsync(query.Page, query.PageSize, cancellationToken);
            return SchoolClassMapper.ToResponseList(schoolClasses);
        }
    }
}
