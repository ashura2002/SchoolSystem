using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;
        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Profile profile)
        {
            _context.Profile.Add(profile);
        }

        public void Remove(Profile profile)
        {
            _context.Profile.Remove(profile);
        }
    }
}
