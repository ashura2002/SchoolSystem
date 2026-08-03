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

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User?> GetByIdWithProfileAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(
                u => u.Id == id && u.DeletedAt == null,
                cancellationToken);
        }

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == UsernameVO.Create(username), cancellationToken);
        }

    }
}
