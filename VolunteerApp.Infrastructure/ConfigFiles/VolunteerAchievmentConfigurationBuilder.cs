using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Aggregates.UserAggregate;
using VolunteerApp.Domain.Entities;

namespace VolunteerApp.Infrastructure.ConfigFiles
{
    public class VolunteerAchievmentConfigurationBuilder
        : IEntityTypeConfiguration<VolunteerAchievement>
    {
        public void Configure(EntityTypeBuilder<VolunteerAchievement> builder)
        {
            builder.ToTable("volunteer_achievments");

            builder.HasKey(a => new { a.VolunteerId, a.TitleDefinitionId });

            builder.HasOne<VolunteerProfile>()
               .WithMany(v => v.Achievements)
               .HasForeignKey(a => a.VolunteerId)
               .IsRequired();

            builder.HasOne<ServiceType>()
                   .WithMany()
                   .HasForeignKey(a => a.TitleDefinitionId)
                   .IsRequired();

            builder.Property(a => a.UnlockedAt).IsRequired();
        }
    }
}