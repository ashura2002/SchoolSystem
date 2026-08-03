using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Teacher.Queries
{
    public class GetTeacherClassByIdHandler:IRequestHandler<GetTeacherClassByIdQuery, TeacherClassDetailDTO>
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEnrollmentReadRespository _enrollmentReadRepository;
        private readonly IUserReadRepository _userReadRepository;


        public GetTeacherClassByIdHandler(
            ISchoolClassRepository schoolClassRepository, 
            ICurrentUserService currentUserService,
            IEnrollmentReadRespository enrollmentRepository, 
            IUserReadRepository userRepository)
        {
            _schoolClassRepository = schoolClassRepository;
            _currentUserService = currentUserService;
            _enrollmentReadRepository = enrollmentRepository;
            _userReadRepository = userRepository;
        }

        public async Task<TeacherClassDetailDTO> Handle(GetTeacherClassByIdQuery request, CancellationToken cancellationToken)
        {
            // find class
            var schoolClass = await _schoolClassRepository.GetClassByIdAsync(request.ClassId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");
             // check ownership
            if (schoolClass.TeacherId != _currentUserService.UserId)
                throw new DomainUnauthorizedException("You are not assigned to this class");

            // get all enrollment by class id and status is == to approved
            var approvedStudentsEnrollment = await _enrollmentReadRepository.GetApprovedEnrollmentStudentByClassIdAsync(request.ClassId,
                cancellationToken);

           //extract ids using select
            var studentIds = approvedStudentsEnrollment.Select(e => e.StudentId).ToList();

            var students = await _userReadRepository.GetUsersByIdsAsync(studentIds, cancellationToken);

            return TeacherClassMapper.ToDto(schoolClass, students);
        }
    }
}