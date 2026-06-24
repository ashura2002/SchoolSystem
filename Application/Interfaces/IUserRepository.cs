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
        Task<User?> GetById(Guid id);
        Task<List<User>> GetAllActiveUsers(PaginationDTO dto);
        Task<List<User>> GetAllDeletedUsers(PaginationDTO dto);
        Task<User?> GetByUsername(string username);
        Task<User?> GetByEmail(string email);
        Task SaveChangesAsync();
        Task<List<User>> GetUsersByIds(List<Guid> ids);
    }
}
