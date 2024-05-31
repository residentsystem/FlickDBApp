using System;
using System.Collections.Generic;

namespace FlickDBLib.Models;

public partial class MovieActor
{
    public int Movieactorid { get; set; }

    public string Character { get; set; } = null!;

    public int Movieid { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public int Actorid { get; set; }

    public virtual Actor Actor { get; set; } = null!;
}
