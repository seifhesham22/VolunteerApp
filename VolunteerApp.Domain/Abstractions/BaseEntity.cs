using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteerApp.Domain.Abstractions
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime? DeletedAt { get; protected set; } 
    }
}