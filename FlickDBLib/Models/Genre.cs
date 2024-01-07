using System;
using System.Collections.Generic;

namespace FlickDBLib.Models;

public partial class Genre
{
    public int Genreid { get; set; }

    public string? Genre1 { get; set; }

    public virtual ICollection<MovieGenre> Moviesgenres { get; } = new List<MovieGenre>();
}
