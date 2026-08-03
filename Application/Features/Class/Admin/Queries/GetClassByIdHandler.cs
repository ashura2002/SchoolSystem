using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Queries
{
    public class GetClassByIdHandler:IRequestHandler<GetClassByIdQuery, SchoolClassDTO>
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public GetClassByIdHandler(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<SchoolClassDTO> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
        {
            var schoolClass = await _schoolClassRepository.GetClassByIdAsync(
                request.ClassId, 
                cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            return SchoolClassMapper.ToDto(schoolClass);
        }
    }
}
