using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ProfileReadRepository : IProfileReadRepository
    {
        private readonly AppDbContext _context;

        public ProfileReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserWithProfileDetailDTO>> GetAllProfiles(int page, int pageSize, CancellationToken cancellationToken)
        {
            return await _context.Profile
                .AsNoTracking()
                .Where(p => p.User.DeletedAt == null)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new UserWithProfileDetailDTO(
                    p.Id,
                    p.UserId,
                    p.User.Username.Value,
                    p.User.Email.Value,
                    p.FirstName.Value,
                    p.LastName.Value,
                    p.Address.Value,
                    p.DateOfBirth,
                    p.ProfilePictureUrl,
                    p.ProfilePicturePublicId
                    ))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<UserWithProfileDetailDTO?> GetMyProfileAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Profile
                .AsNoTracking()
                .Where(p => p.UserId == userId && p.User.DeletedAt == null)
                .Select(p => new UserWithProfileDetailDTO(
                    p.Id,
                    p.UserId,
                    p.User.Username.Value,
                    p.User.Email.Value,
                    p.FirstName.Value,
                    p.LastName.Value,
                    p.Address.Value,
                    p.DateOfBirth,
                    p.ProfilePictureUrl,
                    p.ProfilePicturePublicId
                    ))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<UserWithProfileDetailDTO?> GetProfileByIdAsync(Guid currentUser, Guid profileId, CancellationToken cancellationToken)
        {
            return await _context.Profile
                .AsNoTracking()
                .Where(p => p.UserId == currentUser && p.Id == profileId)
                .Select(p => new UserWithProfileDetailDTO(
                       p.Id,
                    p.UserId,
                    p.User.Username.Value,
                    p.User.Email.Value,
                    p.FirstName.Value,
                    p.LastName.Value,
                    p.Address.Value,
                    p.DateOfBirth,
                    p.ProfilePictureUrl,
                    p.ProfilePicturePublicId
                    ))
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
