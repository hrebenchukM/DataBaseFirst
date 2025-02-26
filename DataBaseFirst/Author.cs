﻿using System;
using System.Collections.Generic;

namespace DataBaseFirst;

public partial class Author
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
