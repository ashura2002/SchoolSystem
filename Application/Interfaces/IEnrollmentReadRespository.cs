using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IEnrollmentReadRespository
    {
        Task<List<EnrollmentDTO>> GetAllPendingEnrollmentsAsync(
            int Page,
            int PageSize, 
            CancellationToken cancellationToken);

        Task<List<EnrollmentDTO>> GetApprovedEnrollmentByStudentIdAsync(
            int Page, 
            int PageSize, 
            Guid studentId, 
            CancellationToken cancellationToken);

        Task<List<EnrollmentDTO>> GetApprovedEnrollmentStudentByClassIdAsync(
            Guid classId, 
            CancellationToken cancellationToken);

        Task<bool> EnrollmentExistsAsync(
            Guid studentId, 
            Guid classId, 
            CancellationToken cancellationToken);
    }
}
