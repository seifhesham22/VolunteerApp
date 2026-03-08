using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Aggregates.UserAggregate;
using VolunteerApp.Domain.Entities;
using VolunteerApp.Domain.Gamefication;

namespace VolunteerApp.Infrastructure.ConfigFiles
{
    public class TitleDefinitionConfigurationBuilder
        : IEntityTypeConfiguration<TitleDefinition>
    {
        public void Configure(EntityTypeBuilder<TitleDefinition> builder)
        {
            builder.ToTable("title_definitions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(t => t.ServiceFilter)
               .WithMany()
               .HasForeignKey("ServiceFilterId")
               .IsRequired(false);
        }
    }
}