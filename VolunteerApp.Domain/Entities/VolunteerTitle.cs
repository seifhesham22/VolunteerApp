using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteerApp.Domain.Entities
{
    public record VolunteerTitle(
        Guid userId,
        int titleDefinitionId,
        DateTime unlockedAt
        );
}