using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Abstractions;

namespace VolunteerApp.Domain.Aggregates.UserAggregate
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string FullName { get; private set; } = null!;
        public string SpokenLanguage { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public VolunteerProfile? VolunteerProfile {  get; private set; }
        public StudentProfile? StudentProfile { get; private set; }

        private User() { }

        public User(
            string name,
            string email,
            string spokenLanguage)
        {
            FullName = name;
            Email = email;
            SpokenLanguage = spokenLanguage;
        }

        public void CreateVolunteerProfile(string bio)
        {
            if (VolunteerProfile != null) throw new InvalidOperationException("Volunteer profile for this user already exists");
            VolunteerProfile = new VolunteerProfile(this.Id, bio);
        }
        public void CreateStudentProfile(string bio)
        {
            if (StudentProfile != null) throw new InvalidOperationException("student profile for this user already exists");
        }
    }
}