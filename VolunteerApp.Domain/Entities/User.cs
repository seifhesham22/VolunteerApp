using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Profiles;

namespace VolunteerApp.Domain.Entities
{
    //public StudentProfile? StudentProfile { get; private set; } to be added later.
    public class User  
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; } = null!;
        public string SpokenLanguage { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public VolunteerProfile? VolunteerProfile {  get; private set; }
    }
}