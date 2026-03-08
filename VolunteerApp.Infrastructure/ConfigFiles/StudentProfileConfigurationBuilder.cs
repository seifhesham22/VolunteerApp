using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Aggregates.UserAggregate;

namespace VolunteerApp.Infrastructure.ConfigFiles
{
    public class StudentProfileConfigurationBuilder
        : IEntityTypeConfiguration<StudentProfile>
    {
        public void Configure(EntityTypeBuilder<StudentProfile> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserId);

            builder.Property(x => x.Bio)
                .HasMaxLength(128)
                .IsRequired(false);
        }
    }
}