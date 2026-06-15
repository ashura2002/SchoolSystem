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
            builder.Property(user => user.Username)
             .HasConversion(v => v.Value, v => UsernameValueObject.Create(v)).IsRequired();
            builder.Property(user => user.Email)
                .HasConversion(v => v.Value, v => EmailValueObject.Create(v)).IsRequired();
            builder.Property(user => user.Password)
                .HasConversion(v => v.Value, v => PasswordValueObject.Create(v)).IsRequired();


            // constraint - unique value
            builder.HasIndex(user => user.Username).IsUnique();
            builder.HasIndex(user => user.Email).IsUnique();
        }
    }
}
