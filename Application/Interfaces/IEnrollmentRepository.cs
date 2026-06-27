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
        Task SaveChangesAsync(CancellationToken cancellationToken);
        void Delete(Enrollment enrollment);
        Task<Enrollment?> GetById(Guid enrollmentId, CancellationToken cancellationToken);
        Task<Enrollment?> GetByStudentAndClass(Guid studentId, Guid classId, CancellationToken cancellationToken);
        Task<List<Enrollment>> GetAllPendingEnrollments(int Page,int PageSize, CancellationToken cancellationToken);
        Task<List<Enrollment>> GetApprovedByStudentId(int Page, int PageSize, Guid studentId, CancellationToken cancellationToken);
        Task<List<Enrollment>> GetApprovedStudentByClassId(Guid classId, CancellationToken cancellationToken);
    }
}
