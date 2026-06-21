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
        Task<List<User>> GetAllUsers(PaginationDTO dto);
        Task Delete(User user);
        Task<User?> GetByUsername(string username);
        Task<User?> GetByEmail(string email);
        Task SaveChangesAsync();
    }
}
