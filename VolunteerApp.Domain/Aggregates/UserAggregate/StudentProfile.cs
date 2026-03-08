using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Abstractions;
using VolunteerApp.Domain.Entities;

namespace VolunteerApp.Domain.Aggregates.UserAggregate
{
    public class StudentProfile : BaseEntity
    {
        public Guid UserId { get; private set; }
        public string Bio { get; private set; } = string.Empty;

        private StudentProfile() { }

        internal StudentProfile(Guid userId,string bio)
        {
            UserId = userId;
            Bio = bio;
        }
    }
}