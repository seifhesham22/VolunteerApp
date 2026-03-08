using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using VolunteerApp.Domain.Abstractions;
using VolunteerApp.Domain.Aggregates.ReviewAggregate;
using VolunteerApp.Domain.CustomExceptions;

namespace VolunteerApp.Domain.Aggregates.UserAggregate
{
    public class VolunteerProfile : BaseEntity
    {
        public Guid UserId { get; private set; }
        public string Bio { get; private set; } = string.Empty;
        public int TotalXp { get; private set; }

        private List<Review> _reviews = new();
        public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

        private readonly List<VolunteerAchievement> _achievements = new();
        public IReadOnlyCollection<VolunteerAchievement> Achievements => _achievements.AsReadOnly();

        public int level => (TotalXp / 100) + 1;

        private VolunteerProfile() { }

        internal VolunteerProfile(Guid UserId, string bio)
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.UtcNow;
            this.Bio = bio;
            this.UserId = UserId;
        }

        public void AddExperience(int amount) => this.TotalXp += amount;

        public void AddReview(Review review)
        {
            _reviews.Add(review);
            TotalXp += 5;
        }

        public void AddTitle(int titleId)
        {
            if (!_achievements.Any(t => t.TitleDefinitionId == titleId))
                _achievements.Add(
                    new VolunteerAchievement(
                    volunteerId: this.UserId,
                    titleId: titleId));
        }
    }
}