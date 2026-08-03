using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class SchoolClassReadRepository : ISchoolClassReadRepository
    {
        private readonly AppDbContext _context;

        public SchoolClassReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SchoolClassDTO>> GetAllClassAsync(
            int Page, 
            int PageSize, 
            CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .OrderByDescending(sc => sc.CreatedAt)
                .Select(sc => new SchoolClassDTO(
                    sc.Id,
                    sc.Name.Value,
                    sc.TeacherId,
                    sc.StartTime,
                    sc.EndTime,
                    sc.Schedule,
                    sc.CreatedAt,
                    sc.UpdatedAt,
                    sc.StudentCapacity,
                    sc.CurrentStudents,
                    sc.RemainingSlots))
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SchoolClassDTO>> GetAllClassesWithoutTeacherAsync(
            int Page, 
            int PageSize, 
            CancellationToken cancellationToken)
        {
           return await _context.SchoolClasses
                .AsNoTracking()
                .Where(sc => sc.TeacherId == null)
                .OrderByDescending(sc => sc.CreatedAt)
                .Select(sc => new SchoolClassDTO(
                    sc.Id,
                    sc.Name.Value,
                    sc.TeacherId,
                    sc.StartTime,
                    sc.EndTime,
                    sc.Schedule,
                    sc.CreatedAt,
                    sc.UpdatedAt,
                    sc.StudentCapacity,
                    sc.CurrentStudents,
                    sc.RemainingSlots))
                 .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SchoolClassDTO>> GetAllClassesWithTeacherAsync(
            int Page, 
            int PageSize, 
            CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .Where(sc => sc.TeacherId != null)
                .OrderByDescending(sc => sc.CreatedAt)
                .Select(sc => new SchoolClassDTO(
                    sc.Id,
                    sc.Name.Value,
                    sc.TeacherId,
                    sc.StartTime,
                    sc.EndTime,
                    sc.Schedule,
                    sc.CreatedAt,
                    sc.UpdatedAt,
                    sc.StudentCapacity,
                    sc.CurrentStudents,
                    sc.RemainingSlots))
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SchoolClassDTO>> GetClassesByIdsAsync(IEnumerable<Guid> schoolId, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .Where(sc => schoolId.Contains(sc.Id))
                .OrderByDescending(sc => sc.CreatedAt)
                .Select(sc => new SchoolClassDTO(
                    sc.Id,
                    sc.Name.Value,
                    sc.TeacherId,
                    sc.StartTime,
                    sc.EndTime,
                    sc.Schedule,
                    sc.CreatedAt,
                    sc.UpdatedAt,
                    sc.StudentCapacity,
                    sc.CurrentStudents,
                    sc.RemainingSlots))
                .ToListAsync(cancellationToken);

        }

        public async Task<List<SchoolClassDTO>> GetOwnClassesAsync(
            int Page, 
            int PageSize, 
            Guid teacherId, 
            CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .AsNoTracking()
                .Where(sc => sc.TeacherId == teacherId)
                .OrderByDescending(sc => sc.CreatedAt)
                .Select(sc => new SchoolClassDTO(
                    sc.Id,
                    sc.Name.Value,
                    sc.TeacherId,
                    sc.StartTime,
                    sc.EndTime,
                    sc.Schedule,
                    sc.CreatedAt,
                    sc.UpdatedAt,
                    sc.StudentCapacity,
                    sc.CurrentStudents,
                    sc.RemainingSlots))
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsTeacherAvailableAsync(
            Guid teacherId, 
            DayOfWeek schedule, 
            TimeOnly startTime, 
            TimeOnly endTime, 
            CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                  .AsNoTracking()
                  .AnyAsync(sc =>
                  sc.TeacherId == teacherId 
                  && sc.Schedule == schedule 
                  && startTime < sc.EndTime // 8:00 < 09:00
                  && endTime > sc.StartTime, // 10:00 > 11:00
                  cancellationToken);
        }
    }
}