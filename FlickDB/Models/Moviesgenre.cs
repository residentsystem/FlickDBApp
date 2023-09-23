using System;
using System.Collections.Generic;

namespace FlickDB.Models;

public partial class Moviesgenre
{
    public int Moviegenreid { get; set; }

    public int Movieid { get; set; }

    public int Genreid { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
