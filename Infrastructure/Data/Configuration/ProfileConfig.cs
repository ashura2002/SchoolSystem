using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configuration
{
    public class ProfileConfig : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .HasConversion(p => p.Value, p => FirstNameVO.Create(p))
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasConversion(p => p.Value, p => LastNameVO.Create(p));

            builder.Property(p => p.Address)
                .HasConversion(p => p.Value, p => AddressVO.Create(p));

            builder.HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
