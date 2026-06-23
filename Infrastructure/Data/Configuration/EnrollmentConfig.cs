using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configuration
{
    public class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => e.Id);

            //  relation on student
            builder.Property(e => e.StudentId).IsRequired();

            // class relation
            builder.Property(e => e.ClassId).IsRequired();

            // Status
            builder.Property(e => e.Status)
                .IsRequired();

            // Index for performance + duplicate prevention
            builder.HasIndex(e => new { e.StudentId, e.ClassId })
                .IsUnique();

            // Optional soft delete filter
            builder.HasQueryFilter(e => e.DeletedAt == null);


        }
    }
}
