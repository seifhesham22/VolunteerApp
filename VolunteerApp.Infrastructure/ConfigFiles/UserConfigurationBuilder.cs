using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Aggregates.UserAggregate;

namespace VolunteerApp.Infrastructure.ConfigFiles
{
    public class UserConfigurationBuilder
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(t => t.Id);

            builder.Property(x => x.SpokenLanguage).IsRequired();

            builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.FullName).IsRequired();

            builder.HasOne(x => x.StudentProfile)
                .WithOne()
                .HasForeignKey<StudentProfile>(x => x.UserId);

            builder.HasOne(x => x.VolunteerProfile)
                .WithOne()
                .HasForeignKey<VolunteerProfile>(x => x.UserId);
        }
    }
}