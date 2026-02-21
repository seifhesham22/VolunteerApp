using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.CustomExceptions;

namespace VolunteerApp.Domain.Entities
{
    public class ServiceType
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public bool IsActive { get; private set; }

        private ServiceType() { }
        public ServiceType(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new DomainException("Service name can't be a null or empty!");
            Name = name;
            IsActive = true;
        }
        public void Rename(string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new DomainException("New service name can't be a null or empty!");
            Name = newName;
        }
        public void DeActivate() => IsActive = false;
        public void ReActivate()
        {
            if (!IsActive)
                IsActive = true;
            else
                throw new DomainException($"the service '{Name}' is active");
        }
    }
}