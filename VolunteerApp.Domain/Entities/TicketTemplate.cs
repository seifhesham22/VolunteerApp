using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.CustomExceptions;

namespace VolunteerApp.Domain.Entities
{
    public class TicketTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        private readonly List<ServiceType> _includedServices = new();
        public IReadOnlyCollection<ServiceType> IncludedServices => _includedServices;

        public TicketTemplate(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new DomainException("Bundle Name can't be empty");

            Name = name;
        }

        public void AddService(ServiceType type)
        {
            if (!_includedServices.Contains(type))
                _includedServices.Add(type);
            else
                throw new DomainException("service already exist in the template");
        }

        public void RemoveService(ServiceType type)
        {
            if (_includedServices.Remove(type))
                _includedServices.Remove(type);
            else
                throw new DomainException($"The service '{type.Name}' is not there already");
        }
    }
}