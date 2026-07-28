using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Queries
{
    public class GetClassesWithoutTeacherHandler:IRequestHandler<GetClassesWithoutTeacherQuery, List<SchoolClassDTO>>
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetClassesWithoutTeacherHandler(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<List<SchoolClassDTO>> Handle(GetClassesWithoutTeacherQuery request, CancellationToken cancellationToken)
        {
            var schoolClasses = await _schoolClassRepository.GetAllClassesWithoutTeacherAsync(request.Page, request.PageSize,
                cancellationToken);
            return SchoolClassMapper.ToResponseList(schoolClasses);
        }
    }
}
