using System;
using System.Collections.Generic;

namespace FlickDB.Models;

public partial class Moviesactor
{
    public int Movieactorid { get; set; }

    public string Character { get; set; } = null!;

    public int Movieid { get; set; }

    public int Actorid { get; set; }

    public virtual Actor Actor { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
