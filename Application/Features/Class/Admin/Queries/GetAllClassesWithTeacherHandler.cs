using Application.DTOs;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Queries
{
    public class GetAllClassesWithTeacherHandler : IRequestHandler<GetAllClassesWithTeacherQuery, List<SchoolClassDTO>>
    {
        private readonly ISchoolClassReadRepository _schoolClassReadRepository;

        public GetAllClassesWithTeacherHandler(ISchoolClassReadRepository schoolClassRepository)
        {
            _schoolClassReadRepository = schoolClassRepository;
        }

        public async Task<List<SchoolClassDTO>> Handle(GetAllClassesWithTeacherQuery request, CancellationToken cancellationToken)
        {
            var schoolClasses = await _schoolClassReadRepository.GetAllClassesWithTeacherAsync(
                request.Page, 
                request.PageSize, 
                cancellationToken);

            return schoolClasses;
        }
    }
}
