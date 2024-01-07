using System;
using System.Collections.Generic;

namespace FlickDBLib.Models;

public partial class MovieGenre
{
    public int Moviegenreid { get; set; }

    public int Movieid { get; set; }

    public int Genreid { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
