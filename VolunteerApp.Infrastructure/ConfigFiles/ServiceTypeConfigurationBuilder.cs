using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Entities;

namespace VolunteerApp.Infrastructure.ConfigFiles
{
    public class ServiceTypeConfigurationBuilder
        : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.ToTable("ServiceTypes");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired();
        }
    }
}