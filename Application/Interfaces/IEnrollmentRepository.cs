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
        Task<Enrollment?> GetEnrollmentByIdAsync(Guid enrollmentId, CancellationToken cancellationToken);
        Task<List<Enrollment>> GetAllPendingEnrollmentsAsync(int Page,int PageSize, CancellationToken cancellationToken);
        Task<List<Enrollment>> GetApprovedEnrollmentByStudentIdAsync(int Page, int PageSize, Guid studentId, CancellationToken cancellationToken);
        Task<List<Enrollment>> GetApprovedEnrollmentStudentByClassIdAsync(Guid classId, CancellationToken cancellationToken);
        Task<Enrollment?> GetEnrollmentByStudentAndClassAsync(Guid studentId, Guid classId, CancellationToken cancellationToken);
    }
}
