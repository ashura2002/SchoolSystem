using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Class.Teacher
{
    public class GetTeacherClassByIdUseCase
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUserRepository _userRepository;


        public GetTeacherClassByIdUseCase(ISchoolClassRepository schoolClassRepository, ICurrentUserService currentUserService,
            IEnrollmentRepository enrollmentRepository, IUserRepository userRepository)
        {
            _schoolClassRepository = schoolClassRepository;
            _currentUserService = currentUserService;
            _enrollmentRepository = enrollmentRepository;
            _userRepository = userRepository;
        }

        public async Task<TeacherClassDetailDTO> Execute(Guid classId, CancellationToken cancellationToken)
        {
            // find class
            var schoolClass = await _schoolClassRepository.GetClassById(classId, cancellationToken) ??
                throw new DomainNotFoundException("Class not found");

            // check ownership
            if (schoolClass.TeacherId != _currentUserService.UserId)
                throw new DomainUnauthorizedException("You are not assigned to this class");

            // get all enrollment by class id and status is == to approved
            var approvedStudentsEnrollment = await _enrollmentRepository.GetApprovedStudentByClassId(classId, cancellationToken);

            //extract ids using select
            var studentIds = approvedStudentsEnrollment.Select(e => e.StudentId).ToList();

            var students = await _userRepository.GetUsersByIds(studentIds, cancellationToken);

            return TeacherClassMapper.ToDto(schoolClass, students);
        }
    }
}