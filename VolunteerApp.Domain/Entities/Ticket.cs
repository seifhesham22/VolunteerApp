using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.Abstractions;
using VolunteerApp.Domain.CustomExceptions;
using VolunteerApp.Domain.Enums;

namespace VolunteerApp.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public Guid StudentId { get; private set; }
        public Guid? VolunteerId { get; private set; }
        public Guid? ChatRoomId { get; private set; }

        public ServiceType ServiceType { get; private set; }
        public string Descreption {  get; private set; } = string.Empty;
        public TicketStatus Status { get; private set; }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        private Ticket() { }
        public Ticket(
            Guid studentId,
            ServiceType serviceType,
            string descreption,
            double latitude,
            double longitude)
        {
            this.StudentId = studentId;
            this.ServiceType = serviceType;
            this.Descreption = descreption;
            this.Latitude = latitude; 
            this.Longitude = longitude;
            this.Status = TicketStatus.Created;
            this.CreatedAt = DateTime.Now;
        }
        public void ClaimTicket(Guid volunteerId, Guid chatRoomId)
        {
            if (this.StudentId == volunteerId)
                throw new DomainException("can't help yourself!");

            if (Status == TicketStatus.Created && VolunteerId is null)
            {
                this.VolunteerId = volunteerId;
                this.ChatRoomId = chatRoomId;
                this.Status = TicketStatus.Claimed;
                this.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                throw new DomainException("can't claim a ticket that is already claimed");
            }
        }
        public void ReleaseTicket(Guid volunteerId)
        {
            if(this.VolunteerId !=  volunteerId)
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
        public void Completed()
        {
            Status = TicketStatus.Completed;
            UpdatedAt = DateTime.UtcNow;
        }
        public void DeleteTicket(Guid studentId)
        {
            if(this.StudentId != studentId)
                throw new DomainException($"the student Id '{studentId}' doesn't own this ticket");

            this.DeletedAt = DateTime.UtcNow;
        }
    }
}