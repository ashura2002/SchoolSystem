using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Queries
{
    public class GetAllClassesWithTeacherHandler : IRequestHandler<GetAllClassesWithTeacherQuery, List<SchoolClassDTO>>
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetAllClassesWithTeacherHandler(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<List<SchoolClassDTO>> Handle(GetAllClassesWithTeacherQuery request, CancellationToken cancellationToken)
        {
            var schoolClasses = await _schoolClassRepository.GetAllClassesWithTeacherAsync(request.Page, request.PageSize, cancellationToken);
            return SchoolClassMapper.ToResponseList(schoolClasses);
        }
    }
}
