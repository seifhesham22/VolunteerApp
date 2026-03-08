using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Aggregates.ReviewAggregate;
using VolunteerApp.Domain.Aggregates.TicketAggregate;
using VolunteerApp.Domain.Aggregates.UserAggregate;
using VolunteerApp.Domain.Entities;

namespace VolunteerApp.Infrastructure
{
    public class VolunteerDbContext : DbContext
    {
        public VolunteerDbContext(DbContextOptions<VolunteerDbContext> options)
        : base(options)
        {
        }

        public DbSet<Ticket> tickets { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<VolunteerProfile> volunteers { get; set; }
        public DbSet<StudentProfile> students { get; set; }
        public DbSet<User> ServiceType { get; set; }
        public DbSet<ServiceType> titleDefinitions { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<VolunteerAchievement> achievements { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("VolunteerApp");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VolunteerDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}