using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.XPath;
using VolunteerApp.Domain.Abstractions;
using VolunteerApp.Domain.CustomExceptions;
using VolunteerApp.Domain.Entities;

namespace VolunteerApp.Domain.Aggregates.TicketAggregate
{
    public class Ticket : BaseEntity, IAggregateRoot
    {
        public Guid StudentId { get; private set; }
        public Guid? VolunteerId { get; private set; }
        public Guid? ChatRoomId { get; private set; }
        public string Title { get; private set; } = null!;
        public int ServiceTypeId { get; private set; }
        public string Descreption { get; private set; } = null!;
        public TicketStatus Status { get; private set; }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        private Ticket() { }

        public static Ticket Create(
            Guid studentId,
            int serviceTypeId,
            string title,
            string description,
            double lat,
            double lon)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new DomainException("Ticket must have a title.");

            return new Ticket
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                ServiceTypeId = serviceTypeId,
                Title = title,
                Descreption = description,
                Latitude = lat,
                Longitude = lon,
                Status = TicketStatus.Created,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Claim(Guid volunteerId, Guid chatRoomId)
        {
            if (Status != TicketStatus.Created)
                throw new DomainException("Ticket is not available for claiming.");

            if (StudentId == volunteerId)
                throw new DomainException("You cannot claim your own ticket.");

            VolunteerId = volunteerId;
            ChatRoomId = chatRoomId;
            Status = TicketStatus.Claimed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ReleaseTicket(Guid volunteerId)
        {
            if (this.VolunteerId != volunteerId)
                throw new DomainException("you don't own this ticket to release it");
            if (this.Status == TicketStatus.Completed)
                throw new DomainException("can't release a ticket that is already completed");

            this.ChatRoomId = null;
            this.VolunteerId = null;
            this.Status = TicketStatus.Created;
            this.UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsCompleted(Guid volunteerId)
        {
            if (this.VolunteerId != volunteerId)
                throw new DomainException($"The volunteer with Id '{volunteerId}' is not assigned to this ticket");

            if (this.Status == TicketStatus.Completed)
                throw new DomainException("can't complete a ticket that is already completed");

            this.Status = TicketStatus.Completed;
            this.UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteTicket(Guid studentId)
        {
            if (this.StudentId != studentId)
                throw new DomainException($"the student Id '{studentId}' doesn't own this ticket");

            this.DeletedAt = DateTime.UtcNow;
        }
    }
}