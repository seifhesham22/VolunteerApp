using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteerApp.Domain.CustomExceptions
{
    public class DomainException(string message) : Exception(message);
}