using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User?> GetById(Guid userId, CancellationToken cancellationToken);
        Task<List<User>> GetAllActiveUsers(int Page, int PageSize, CancellationToken cancellationToken);
        Task<List<User>> GetAllDeletedUsers(int Page, int PageSize, CancellationToken cancellationToken);
        Task<User?> GetByUsername(string username, CancellationToken cancellationToken);
        Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<List<User>> GetUsersByIds(List<Guid> ids, CancellationToken cancellationToken);
    }
}
