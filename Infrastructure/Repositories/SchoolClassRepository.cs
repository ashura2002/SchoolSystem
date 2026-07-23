using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class SchoolClassRepository(AppDbContext appDbContext) : ISchoolClassRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public void Add(SchoolClass schoolClass)
        {
            _context.SchoolClasses.Add(schoolClass);
        }

        public void Remove(SchoolClass schoolClass)
        {
            _context.SchoolClasses.Remove(schoolClass);
        }

        public async Task<List<SchoolClass>> GetAllClassAsync(int Page, int PageSize, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .OrderByDescending(sc => sc.CreatedAt)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SchoolClass>> GetAllClassesByIds(List<Guid> schoolClassId, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses.AsNoTracking()
                .OrderByDescending(sc => sc.CreatedAt)
                .Where(sc => schoolClassId.Contains(sc.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SchoolClass>> GetAllClassesWithoutTeacherAsync(int Page, int PageSize, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .OrderByDescending(sc => sc.CreatedAt)
                .Where(sc => sc.TeacherId == null)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SchoolClass>> GetAllClassesWithTeacherAsync(int Page, int PageSize, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .OrderByDescending(sc => sc.CreatedAt)
                .Where(sc => sc.TeacherId != null)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<SchoolClass?> GetClassByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .FirstOrDefaultAsync(sc => sc.Id == id, cancellationToken);
        }

        public async Task<List<SchoolClass>> GetClassesByIdsAsync(IEnumerable<Guid> schoolId, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .OrderByDescending(e => e.CreatedAt)
                .Where(e => schoolId.Contains(e.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SchoolClass>> GetOwnClassesAsync(int Page, int PageSize, Guid teacherId, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .OrderByDescending(sc => sc.CreatedAt)
                .Where(sc => sc.TeacherId == teacherId)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
