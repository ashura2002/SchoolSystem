using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserReadRepository
    {
        Task<List<UserDTO>> GetAllActiveUsersAsync(int Page, int PageSize, CancellationToken cancellationToken);
        Task<List<UserDTO>> GetAllDeletedUsersAsync(int Page, int PageSize, CancellationToken cancellationToken);
        Task<UserDTO?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<List<UserDTO>> GetUsersByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
        Task<List<UserDTO>> GetAllAdminsAsync(CancellationToken cancellationToken);
        Task<bool> IsUsernameExistsAsync(string username, CancellationToken cancellationToken);
        Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken);
    }
}
