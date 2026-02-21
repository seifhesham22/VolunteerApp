using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using VolunteerApp.Domain.Abstractions;
using VolunteerApp.Domain.CustomExceptions;
using VolunteerApp.Domain.Entities;
using VolunteerApp.Domain.Enums;

namespace VolunteerApp.Domain.Profiles
{
    public class VolunteerProfile : BaseEntity
    {
        public Guid UserId { get; private set; }
        public string Bio { get; private set; } = string.Empty;
        public int TotalXp { get; private set; }

        private List<Review> _reviews = new();
        public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

        private List<VolunteerTitle> _unlockedTitles = new();
        public IReadOnlyCollection<VolunteerTitle> EarnedTitles => _unlockedTitles.AsReadOnly();

        public int level => (TotalXp / 100) + 1;

        private VolunteerProfile() { }

        public VolunteerProfile(string bio)
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.UtcNow;
            this.Bio = bio;
        }

        public void AddExperience(int amount) => this.TotalXp += amount;

        public void AddReview(Review review)
        {
            _reviews.Add(review);
            TotalXp += 5;
        }

        public void AddTitle(int titleId)
        {
            if (!_unlockedTitles.Any(t => t.titleDefinitionId == titleId))
                _unlockedTitles.Add(
                    new VolunteerTitle(
                    userId: this.UserId,
                    titleDefinitionId: titleId,
                    DateTime.UtcNow));
        }
    }
}