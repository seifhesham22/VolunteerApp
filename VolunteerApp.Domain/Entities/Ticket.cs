using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.XPath;
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
        public string Title { get; private set; } = null!;
        public ServiceType? SingleServiceType { get; private set; }

        public Guid? ParentTicketId { get; private set; }
        public Ticket? ParentTicket { get; private set; }

        private List<Ticket> _subTickets = new();
        public IReadOnlyCollection<Ticket> SubTickets => _subTickets;

        public string Descreption { get; private set; } = string.Empty;
        public TicketStatus Status { get; private set; }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        private Ticket() { }

        public static Ticket CreateSingle(
            Guid studentId,
            ServiceType service,
            double lat,
            double lon)
        {
            return new Ticket()
            {
                StudentId = studentId,
                SingleServiceType = service,
                Title = service.Name,
                Latitude = lat,
                Longitude = lon,
                Status = TicketStatus.Created,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public static Ticket CreateCustomBundle(
            Guid studentId,
            List<ServiceType> requestedServices,
            double lat,
            double lon)
        {
            if (requestedServices.Count < 2)
                throw new DomainException("A bundle must have at least 2 services");

            var parent = new Ticket
            {
                StudentId = studentId,
                SingleServiceType = null,
                Title = $"{requestedServices.First().Name} & {requestedServices.Count - 1} others",
                Latitude = lat,
                Longitude = lon,
                Status = TicketStatus.Created,
                CreatedAt = DateTime.UtcNow,
            };

            foreach (var service in requestedServices)
            {
                parent._subTickets.Add(CreateSingle(studentId, service, lat, lon));
            }
            return parent;
        }

        public static Ticket CreateTemplateBundle(
            Guid studentId,
            string templateName,
            List<ServiceType> templateServices,
            double lat,
            double lon)
        {
            var parent = new Ticket
            {
                StudentId = studentId,
                SingleServiceType = null,
                Title = templateName,
                Latitude = lat,
                Longitude = lon,
                Status = TicketStatus.Created
            };

            foreach (var service in templateServices)
            {
                parent._subTickets.Add(CreateSingle(studentId, service, lat, lon));
            }

            return parent;
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