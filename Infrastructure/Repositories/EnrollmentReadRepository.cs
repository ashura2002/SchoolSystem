using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class EnrollmentReadRepository : IEnrollmentReadRespository
    {
        private readonly AppDbContext _context;

        public EnrollmentReadRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<bool> EnrollmentExistsAsync(
            Guid studentId, 
            Guid classId, 
            CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .AnyAsync(e => e.StudentId == studentId &&  e.ClassId == classId, 
                cancellationToken);
        }

        public async Task<List<EnrollmentDTO>> GetAllPendingEnrollmentsAsync(
            int Page, 
            int PageSize, 
            CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                 .AsNoTracking()
                 .Where(e => e.Status == EnrollmentStatus.Pending)
                 .OrderByDescending(e => e.CreatedAt)
                 .Select(e => new EnrollmentDTO(
                     e.Id,
                     e.StudentId,
                     e.ClassId,
                     e.Status,
                     e.CreatedAt,
                     e.UpdatedAt,
                     e.DeletedAt
                     ))
                 .Skip((Page - 1) * PageSize)
                 .Take(PageSize)
                 .ToListAsync(cancellationToken);
        }

        public async Task<List<EnrollmentDTO>> GetApprovedEnrollmentByStudentIdAsync(
            int Page, 
            int PageSize, 
            Guid studentId, 
            CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Where(e => e.StudentId == studentId && e.Status == EnrollmentStatus.Approved)
                .OrderByDescending(e => e.CreatedAt)
                .Select(e => new EnrollmentDTO(
                     e.Id,
                     e.StudentId,
                     e.ClassId,
                     e.Status,
                     e.CreatedAt,
                     e.UpdatedAt,
                     e.DeletedAt
                    ))
                 .Skip((Page - 1) * PageSize)
                 .Take(PageSize)
                 .ToListAsync(cancellationToken);
        }

        public async Task<List<EnrollmentDTO>> GetApprovedEnrollmentStudentByClassIdAsync(
            Guid classId, 
            CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Where(e => e.ClassId == classId && e.Status == EnrollmentStatus.Approved)
                .Select(e => new EnrollmentDTO(
                     e.Id,
                     e.StudentId,
                     e.ClassId,
                     e.Status,
                     e.CreatedAt,
                     e.UpdatedAt,
                     e.DeletedAt
                    ))
                .ToListAsync(cancellationToken);
        }
    }
}
