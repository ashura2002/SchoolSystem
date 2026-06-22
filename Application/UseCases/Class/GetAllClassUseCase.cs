using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class
{
    public class GetAllClassUseCase
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetAllClassUseCase(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }


        public async Task<IEnumerable<SchoolClassDTO>> Execute(PaginationDTO pagination)
        {
            var schoolClasses = await _schoolClassRepository.GetAllClass(pagination);
            return SchoolClassMapper.ToResponseList(schoolClasses);
        }

    }
}
