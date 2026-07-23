using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Queries
{
    public class GetAllClassHandler
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetAllClassHandler(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }


        public async Task<List<SchoolClassDTO>> Handle(GetAllClassesQuery query, CancellationToken cancellationToken)
        {
            var schoolClasses = await _schoolClassRepository.GetAllClassAsync(query.Page, query.PageSize, cancellationToken);
            return SchoolClassMapper.ToResponseList(schoolClasses);
        }

    }
}
