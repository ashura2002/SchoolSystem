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

        Task<Enrollment?> GetEnrollmentByIdAsync(
            Guid enrollmentId, 
            CancellationToken cancellationToken);
    }
}
