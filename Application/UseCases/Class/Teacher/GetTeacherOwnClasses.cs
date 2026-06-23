using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class.Teacher
{
    public class GetTeacherOwnClasses
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTeacherOwnClasses(ISchoolClassRepository schoolClassRepository, ICurrentUserService currentUserService)
        {
            _schoolClassRepository = schoolClassRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<SchoolClassDTO>> Execute(PaginationDTO pagination)
        {
            var teacherId = _currentUserService.UserId;
            var schoolClasses = await _schoolClassRepository.GetOwnClasses(pagination, teacherId);
            return SchoolClassMapper.ToResponseList(schoolClasses);
        }
    }
}
