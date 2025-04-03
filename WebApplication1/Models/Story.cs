using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Story
{
    public int? Id { get; set; }

    public string? Title { get; set; }

    public DateTime? PublishStart { get; set; }

    public DateTime? PublishEnd { get; set; }

    public int? CategoryId { get; set; }

    public int? AuthorId { get; set; }
}
