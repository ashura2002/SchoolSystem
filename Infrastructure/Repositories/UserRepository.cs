using Application.DTOs;
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


        public async Task<List<User>> GetAllActiveUsers(PaginationDTO pagination)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .OrderByDescending(u => u.CreatedAt)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .AsNoTracking().ToListAsync();
        }

        public async Task<List<User>> GetAllDeletedUsers(PaginationDTO pagination)
        {
            return await _context.Users
              .Where(u => u.DeletedAt != null)
              .OrderByDescending(u => u.CreatedAt)
              .Skip((pagination.Page - 1) * pagination.PageSize)
              .Take(pagination.PageSize)
              .AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == EmailValueObject.Create(email));
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == UsernameValueObject.Create(username));
        }

        public async Task<List<User>> GetUsersByIds(List<Guid> ids)
        {
            return await _context.Users
                  .Where(u => ids.Contains(u.Id))
                  .AsNoTracking()
                  .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
