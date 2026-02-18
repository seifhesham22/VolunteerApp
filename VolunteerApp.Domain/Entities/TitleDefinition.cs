using System;
using System.Collections.Generic;
using System.Text;
using VolunteerApp.Domain.CustomExceptions;
using VolunteerApp.Domain.Enums;

namespace VolunteerApp.Domain.Entities
{
    public class TitleDefinition
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public CriteriaType CriteriaType { get; private set; }
        public int RequiredValue { get; private set; }
        public ServiceType ServiceFilter { get; private set; }
        public string TitlePadge { get; private set; }

        private TitleDefinition() { }

        public TitleDefinition(
            string name,
            CriteriaType criteriaType,
            int requiredValue,
            ServiceType serviceType,
            string titlePadge)
        {
            if (string.IsNullOrEmpty(name)) throw new DomainException("Title Name can't be null or empty");
            if (string.IsNullOrEmpty(titlePadge)) throw new DomainException("Title Padge Name can't be null or empty");
            Name = name;
            CriteriaType = criteriaType;
            RequiredValue = requiredValue;
            ServiceFilter = serviceType;
            TitlePadge = titlePadge;
        }
    }
}