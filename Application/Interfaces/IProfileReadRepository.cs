using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IProfileReadRepository
    {
        Task<UserWithProfileDetailDTO?> GetProfileByIdAsync(Guid userId, Guid profileId, CancellationToken cancellationToken);
        Task<UserWithProfileDetailDTO?> GetMyProfileAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<UserWithProfileDetailDTO>> GetAllProfiles(int page, int pageSize, CancellationToken cancellationToken);
    }
}
