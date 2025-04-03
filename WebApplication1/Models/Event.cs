using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Event
{
    public int? Id { get; set; }

    public string? Title { get; set; }

    public DateTime? EventDateTime { get; set; }

    public DateTime? RegistrationDeadline { get; set; }

    public int? AuthorId { get; set; }
}
