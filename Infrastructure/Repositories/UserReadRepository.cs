using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Domain.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly AppDbContext _context;
        public UserReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
          .AsNoTracking()
          .Where(u => u.DeletedAt == null)
          .AnyAsync(u => u.Email == EmailVO.Create(email), cancellationToken);
        }

        public async Task<List<UserDTO>> GetAllActiveUsersAsync(int Page, int PageSize, CancellationToken cancellationToken)
        {
            return await _context.Users
               .AsNoTracking()
               .Where(u => u.DeletedAt == null)
               .OrderByDescending(u => u.CreatedAt)
               .Select(u => new UserDTO(
                 u.Id,
                 u.Username.Value,
                 u.Email.Value,
                 u.Role,
                 u.CreatedAt,
                 u.UpdatedAt,
                 u.DeletedAt
                   ))
               .Skip((Page - 1) * PageSize)
               .Take(PageSize)
               .ToListAsync(cancellationToken);
        }

        public async Task<List<UserDTO>> GetAllAdminsAsync(CancellationToken cancellationToken)
        {
            return await _context.Users
               .AsNoTracking()
               .Where(u => u.Role == Role.Admin)
               .OrderByDescending(u => u.CreatedAt)
               .Select(u => new UserDTO(
                 u.Id,
                 u.Username.Value,
                 u.Email.Value,
                 u.Role,
                 u.CreatedAt,
                 u.UpdatedAt,
                 u.DeletedAt
                   ))
               .ToListAsync(cancellationToken);
        }

        public async Task<List<UserDTO>> GetAllDeletedUsersAsync(int Page, int PageSize, CancellationToken cancellationToken)
        {
            return await _context.Users
              .AsNoTracking()
              .Where(u => u.DeletedAt != null)
              .OrderByDescending(u => u.CreatedAt)
              .Select(u => new UserDTO(
                u.Id,
                u.Username.Value,
                u.Email.Value,
                u.Role,
                u.CreatedAt,
                u.UpdatedAt,
                u.DeletedAt
                  ))
              .Skip((Page - 1) * PageSize)
              .Take(PageSize)
              .ToListAsync(cancellationToken);
        }

        public async Task<UserDTO?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
              .AsNoTracking()
              .Select(u => new UserDTO(
                  u.Id,
                  u.Username.Value,
                  u.Email.Value,
                  u.Role,
                  u.CreatedAt,
                  u.UpdatedAt,
                  u.DeletedAt))
              .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<List<UserDTO>> GetUsersByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            return await _context.Users
                 .AsNoTracking() 
                 .Where(u => ids.Contains(u.Id) && u.DeletedAt == null)
                 .Select(u => new UserDTO(
                      u.Id,
                      u.Username.Value,
                      u.Email.Value,
                      u.Role,
                      u.CreatedAt,
                      u.UpdatedAt,
                      u.DeletedAt))
                 .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsUsernameExistsAsync(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.DeletedAt == null)
                .AnyAsync(u => u.Username == UsernameVO.Create(username), cancellationToken);
        }
    }
}
