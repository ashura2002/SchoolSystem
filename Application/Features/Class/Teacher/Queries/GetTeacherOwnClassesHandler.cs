using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Teacher.Queries
{
    public class GetTeacherOwnClassesHandler : IRequestHandler<GetTeacherOwnClassesQuery, List<SchoolClassDTO>>
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTeacherOwnClassesHandler(ISchoolClassRepository schoolClassRepository, ICurrentUserService currentUserService)
        {
            _schoolClassRepository = schoolClassRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<SchoolClassDTO>> Handle(GetTeacherOwnClassesQuery request, CancellationToken cancellationToken)
        {
            var teacherId = _currentUserService.UserId;
            var schoolClasses = await _schoolClassRepository.GetOwnClassesAsync(request.Page, request.PageSize, teacherId, cancellationToken);
            return SchoolClassMapper.ToResponseList(schoolClasses);
        }
    }
}
