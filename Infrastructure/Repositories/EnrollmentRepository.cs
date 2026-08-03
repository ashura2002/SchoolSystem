using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {

        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }


        public void Add(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
        }

        public void Delete(Enrollment enrollment)
        {
            _context.Enrollments.Remove(enrollment);
        }

        public async Task<Enrollment?> GetEnrollmentByIdAsync(
            Guid enrollmentId, 
            CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .FirstOrDefaultAsync(e => e.Id == enrollmentId, cancellationToken);
        }
    }
}
