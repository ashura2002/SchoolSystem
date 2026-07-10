using Domain.Enums;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {

        private readonly AppDbContext _context = context;

        public async Task Add(User user)
        {
            _context.Users.Add(user);
        }


        public async Task<List<User>> GetAllActiveUsers(int Page, int PageSize, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .OrderByDescending(u => u.CreatedAt)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<List<User>> GetAllAdmins(CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.Role == Role.Admin)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<User>> GetAllDeletedUsers(int Page, int PageSize, CancellationToken cancellationToken)
        {
            return await _context.Users
              .Where(u => u.DeletedAt != null)
              .OrderByDescending(u => u.CreatedAt)
              .Skip((Page - 1) * PageSize)
              .Take(PageSize)
              .AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == EmailValueObject.Create(email), cancellationToken);
        }

        public async Task<User?> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User?> GetByUsername(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == UsernameValueObject.Create(username), cancellationToken);
        }

        public async Task<List<User>> GetUsersByIds(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            return await _context.Users
                  .Where(u => ids.Contains(u.Id))
                  .AsNoTracking()
                  .ToListAsync(cancellationToken);
        }

    }
}
