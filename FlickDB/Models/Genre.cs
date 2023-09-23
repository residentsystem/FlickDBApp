using System;
using System.Collections.Generic;

namespace FlickDB.Models;

public partial class Genre
{
    public int Genreid { get; set; }

    public string? Genre1 { get; set; }

    public virtual ICollection<Moviesgenre> Moviesgenres { get; } = new List<Moviesgenre>();
}
