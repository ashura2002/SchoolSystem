using Application.DTOs;
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

        public async Task<List<Enrollment>> GetAllPendingEnrollments(PaginationDTO pagination, CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Where(e => e.Status == EnrollmentStatus.Pending)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Enrollment>> GetApprovedByStudentId(PaginationDTO pagination, Guid studentId,
            CancellationToken cancellationToken)
        {
            return await _context.Enrollments
             .AsNoTracking()
             .Where(e => e.StudentId == studentId && e.Status == EnrollmentStatus.Approved)
             .OrderByDescending(e => e.CreatedAt)
             .Skip((pagination.Page - 1) * pagination.PageSize)
             .Take(pagination.PageSize)
             .ToListAsync(cancellationToken);
        }

        public async Task<List<Enrollment>> GetApprovedStudentByClassId(Guid classId, CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Where(e => e.ClassId == classId && e.Status == EnrollmentStatus.Approved)
                .ToListAsync(cancellationToken);
        }

        public async Task<Enrollment?> GetById(Guid enrollmentId, CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .FirstOrDefaultAsync(e => e.Id == enrollmentId, cancellationToken);
        }

        public async Task<Enrollment?> GetByStudentAndClass(Guid studentId, Guid classId, CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .AsNoTracking()
               .FirstOrDefaultAsync(e => e.StudentId == studentId && e.ClassId == classId, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
