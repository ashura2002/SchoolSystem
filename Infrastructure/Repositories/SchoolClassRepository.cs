using Application.DTOs;
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

        public async Task AddClass(SchoolClass schoolClass)
        {
            _context.SchoolClasses.Add(schoolClass);
        }

        public void DeleteClass(SchoolClass schoolClass)
        {
            _context.SchoolClasses.Remove(schoolClass);
        }

        public async Task<List<SchoolClass>> GetAllClass(PaginationDTO pagination,CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .OrderByDescending(sc => sc.CreatedAt)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SchoolClass>> GetAllClassesWithoutTeacher(PaginationDTO pagination, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .OrderByDescending(sc => sc.CreatedAt)
                .Where(sc => sc.TeacherId == null)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SchoolClass>> GetAllClassesWithTeacher(PaginationDTO pagination, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .OrderByDescending(sc => sc.CreatedAt)
                .Where(sc => sc.TeacherId != null)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<SchoolClass?> GetClassById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .FirstOrDefaultAsync(sc => sc.Id == id, cancellationToken);
        }

        public async Task<List<SchoolClass>> GetOwnClasses(PaginationDTO pagination, Guid teacherId, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .OrderByDescending(sc => sc.CreatedAt)
                .Where(sc => sc.TeacherId == teacherId)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task SaveChangesClassAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
