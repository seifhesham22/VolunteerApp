using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteerApp.Domain.Enums
{
    public enum TicketStatus
    {
        Created = 0,
        Claimed = 1,
        Reported = 2,
        InProcess = 3,
        Completed = 4,
    }
}