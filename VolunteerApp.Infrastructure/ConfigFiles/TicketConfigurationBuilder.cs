using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Aggregates.TicketAggregate;
using VolunteerApp.Domain.Aggregates.UserAggregate;
using VolunteerApp.Domain.Entities;

namespace VolunteerApp.Infrastructure.ConfigFiles
{
    public class TicketConfigurationBuilder
        : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("tickets");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.VolunteerId);

            builder.HasIndex(x => x.ChatRoomId);

            builder.Property(x => x.Title)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion(typeof(string))
                .IsRequired();

            builder.Property(x => x.Descreption)
                .HasMaxLength(500);

            builder.Property(x => x.Latitude)
                .IsRequired();

            builder.Property(x => x.Longitude)
                .IsRequired();

            builder.HasOne<ServiceType>()
                .WithMany()
                .HasForeignKey(x => x.ServiceTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<VolunteerProfile>()
                .WithMany()
                .HasForeignKey(x => x.VolunteerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne<StudentProfile>()
                .WithMany()
                .HasForeignKey(x => x.StudentId);
        }
    }
}