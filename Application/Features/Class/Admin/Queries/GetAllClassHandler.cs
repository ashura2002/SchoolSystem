using Application.DTOs;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Queries
{
    public class GetAllClassHandler : IRequestHandler<GetAllClassesQuery, List<SchoolClassDTO>>
    {
        private readonly ISchoolClassReadRepository _schoolClassReadRepository;

        public GetAllClassHandler(ISchoolClassReadRepository schoolClassReadRepository)
        {
            _schoolClassReadRepository = schoolClassReadRepository;
        }

        public async Task<List<SchoolClassDTO>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            var schoolClasses = await _schoolClassReadRepository.GetAllClassAsync(
                request.Page, 
                request.PageSize,
                cancellationToken);

            return schoolClasses;

        }
    }
}
