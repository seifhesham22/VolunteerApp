using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Aggregates.UserAggregate;

namespace VolunteerApp.Infrastructure.ConfigFiles
{
    public class VolunteerConfigurationBuilder
        : IEntityTypeConfiguration<VolunteerProfile>
    {
        public void Configure(EntityTypeBuilder<VolunteerProfile> builder)
        {
            builder.ToTable("Volunteers");
            builder.HasKey(vp => vp.Id);
        }
    }
}