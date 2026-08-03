using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        // for getting profile
        Task<User?> GetByIdWithProfileAsync(Guid userId, CancellationToken cancellationToken);
        Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);

    }
}
