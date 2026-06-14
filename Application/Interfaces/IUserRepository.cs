using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User?> GetById(Guid id);
        Task<List<User>> GetAllUsers();
        Task<User> Update(User user);
        Task Delete(User user);
        Task<User?> GetByUsername(string username);
        Task<User?> GetByEmail(string email);
    }
}
