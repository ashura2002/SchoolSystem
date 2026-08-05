using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public DatabaseSeeder(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }


        public async Task SeedAsync()
        {
            // Seed only when the Users table is empty.
            var exists = await _context.Users.AnyAsync();

            if (exists)
                return;

            var hashedPassword = _passwordHasher.Hash("admin123!");

            var admin = User.Register(
                 UsernameVO.Create("seeded"),
                 EmailVO.Create("seeded@gmail.com"),
                 PasswordVO.Create(hashedPassword),
                 Role.Admin);

            // Create a default profile for the seeded admin
            admin.CreateProfile(
                FirstNameVO.Create("System"),
                LastNameVO.Create("Administrator"),
                AddressVO.Create("Unknown Address N/A"),
                new DateOnly(2000, 1, 1));

            _context.Users.Add(admin);

            await _context.SaveChangesAsync();
        }

    }
}
