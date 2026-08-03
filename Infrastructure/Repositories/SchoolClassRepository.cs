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
        public async Task<SchoolClass?> GetClassByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.SchoolClasses
                .FirstOrDefaultAsync(sc => sc.Id == id, cancellationToken);
        }
    }
}
