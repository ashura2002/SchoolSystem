using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Queries
{
    public class GetClassByIdHandler
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetClassByIdHandler(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<SchoolClassDTO> Handle(GetClassByIdQuery query, CancellationToken cancellationToken)
        {
            var schoolClass = await _schoolClassRepository.GetClassById(query.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");
            return SchoolClassMapper.ToDto(schoolClass);
        }

    }
}
