using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<User>> GetAllActiveUsersAsync(int Page, int PageSize, CancellationToken cancellationToken);
        Task<List<User>> GetAllDeletedUsersAsync(int Page, int PageSize, CancellationToken cancellationToken);
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<List<User>> GetUsersByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
        Task<List<User>> GetAllAdminsAsync(CancellationToken cancellationToken);
    }
}
