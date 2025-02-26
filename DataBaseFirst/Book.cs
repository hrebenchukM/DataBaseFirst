using System;
using System.Collections.Generic;

namespace DataBaseFirst;

public partial class Book
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? AuthorId { get; set; }

    public virtual Author? Author { get; set; }
}
