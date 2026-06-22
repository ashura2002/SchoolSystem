using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(u => u.Username)
                .HasConversion(v => v.Value, v => UsernameValueObject.Create(v))
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasConversion(v => v.Value, v => EmailValueObject.Create(v))
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasConversion(v => v.Value, v => PasswordValueObject.Create(v))
                .HasMaxLength(255)
                .IsRequired();

            // for constraint unique values
            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
