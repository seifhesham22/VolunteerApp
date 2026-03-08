using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Aggregates.ReviewAggregate;
using VolunteerApp.Domain.Aggregates.UserAggregate;

namespace VolunteerApp.Infrastructure.ConfigFiles
{
    public class ReviewConfigurationBuilder
        : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");

            builder.HasKey(x => x.Id);

            builder.HasOne<VolunteerProfile>()
           .WithMany()
           .HasForeignKey(x => x.VolunteerId)
           .IsRequired();

            builder.HasOne<User>()
           .WithMany()
           .HasForeignKey(x => x.AuthorId)
           .IsRequired();

            builder.HasIndex(x => new { x.AuthorId, x.VolunteerId }).IsUnique(); // for spaming issues
        }
    }
}