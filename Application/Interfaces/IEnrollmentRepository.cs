using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IEnrollmentRepository
    {
        void Add(Enrollment enrollment);
        Task SaveChangesAsync();
        void Delete(Enrollment enrollment);
        Task<Enrollment?> GetById(Guid enrollmentId);
        Task<Enrollment?> GetByStudentAndClass(Guid studentId, Guid classId);
        Task<List<Enrollment>> GetAllPendingEnrollments(PaginationDTO pagination);
        Task<List<Enrollment>> GetApprovedByStudentId(PaginationDTO pagination, Guid studentId);
    }
}
