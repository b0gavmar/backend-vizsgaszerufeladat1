using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Participant
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? Identifier { get; set; }

    public string? Email { get; set; }
}
