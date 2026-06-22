using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configuration
{
    public class SchoolClassConfig : IEntityTypeConfiguration<SchoolClass>
    {
        public void Configure(EntityTypeBuilder<SchoolClass> builder)
        {
            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.Name).HasConversion(v => v.Value, v => ClassNameValueObject.Create(v))
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
