using Application.DTOs;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Teacher.Queries
{
    public class GetTeacherOwnClassesHandler : IRequestHandler<GetTeacherOwnClassesQuery, List<SchoolClassDTO>>
    {
        private readonly ISchoolClassReadRepository _schoolClassReadRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTeacherOwnClassesHandler(
            ISchoolClassReadRepository schoolClassRepository, 
            ICurrentUserService currentUserService)
        {
            _schoolClassReadRepository = schoolClassRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<SchoolClassDTO>> Handle(GetTeacherOwnClassesQuery request, CancellationToken cancellationToken)
        {
            var teacherId = _currentUserService.UserId;
            var schoolClasses = await _schoolClassReadRepository.GetOwnClassesAsync(request.Page, 
                request.PageSize, 
                teacherId, 
                cancellationToken);

            return schoolClasses;
        }
    }
}
