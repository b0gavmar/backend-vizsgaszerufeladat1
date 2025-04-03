using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Registration
{
    public int? ParticipantId { get; set; }

    public int? EventId { get; set; }

    public DateTime? RegistrationDateTime { get; set; }
}
