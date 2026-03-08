using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Abstractions;

namespace VolunteerApp.Domain.Aggregates.UserAggregate
{
    public class VolunteerAchievement
    {
        public Guid VolunteerId { get; private set; }
        public int TitleDefinitionId { get; private set; }
        public DateTime UnlockedAt { get; private set; }

        private VolunteerAchievement() { }

        public VolunteerAchievement(Guid volunteerId, int titleId)
        {
            VolunteerId = volunteerId;
            TitleDefinitionId = titleId;
            UnlockedAt = DateTime.UtcNow;
        }
    }
}