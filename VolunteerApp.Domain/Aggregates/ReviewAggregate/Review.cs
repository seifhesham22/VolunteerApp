using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using VolunteerApp.Domain.Abstractions;
using VolunteerApp.Domain.CustomExceptions;

namespace VolunteerApp.Domain.Aggregates.ReviewAggregate
{
    public class Review : BaseEntity, IAggregateRoot
    {
        public Guid AuthorId { get; set; }
        public Guid VolunteerId { get;set; }
        public string Content { get; set; } = null!;

        private Review() { }
        public Review(
            Guid ticketId,
            Guid authorId,
            Guid volunteerId,
            string content)
        {
            this.AuthorId = authorId;
            this.VolunteerId = volunteerId;
            this.Content = content;
        }
        public void UpdateReview(
            Guid authorId,
            Guid volunteerId,
            string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new DomainException("Content cannot be null or empty!");

            if (!EnsureOwner(authorId, volunteerId))
                throw new DomainException("Unauthorized!");

            this.Content = content;
            this.UpdatedAt = DateTime.Now;
        }
        public void DeleteReview(Guid authorId, Guid volunteerId)
        {
            if (!EnsureOwner(authorId,volunteerId))
                throw new DomainException("Unauthorized!");

            this.DeletedAt = DateTime.Now;
        }
        
        private bool EnsureOwner(Guid authorId, Guid volunteerId)
        {
            if (this.AuthorId != authorId || this.VolunteerId != volunteerId)
                return false;

            return true;
        }
    }
}