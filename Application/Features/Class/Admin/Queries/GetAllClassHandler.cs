using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Queries
{
    public class GetAllClassHandler : IRequestHandler<GetAllClassesQuery, List<SchoolClassDTO>>
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetAllClassHandler(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<List<SchoolClassDTO>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            var schoolClasses = await _schoolClassRepository.GetAllClassAsync(request.Page, request.PageSize, cancellationToken);
            return SchoolClassMapper.ToResponseList(schoolClasses);

        }
    }
}
