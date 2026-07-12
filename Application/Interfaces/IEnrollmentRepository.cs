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
        void Delete(Enrollment enrollment);
        Task<Enrollment?> GetByIdAsync(Guid enrollmentId, CancellationToken cancellationToken);
        Task<List<Enrollment>> GetAllPendingEnrollmentsAsync(int Page,int PageSize, CancellationToken cancellationToken);
        Task<List<Enrollment>> GetApprovedByStudentIdAsync(int Page, int PageSize, Guid studentId, CancellationToken cancellationToken);
        Task<List<Enrollment>> GetApprovedStudentByClassIdAsync(Guid classId, CancellationToken cancellationToken);
        Task<Enrollment?> GetByStudentAndClassAsync(Guid studentId, Guid classId, CancellationToken cancellationToken);
    }
}
