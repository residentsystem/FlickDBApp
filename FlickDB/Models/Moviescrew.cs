using System;
using System.Collections.Generic;

namespace FlickDB.Models;

public partial class Moviescrew
{
    public int Moviecrewid { get; set; }

    public int Movieid { get; set; }

    public int Crewid { get; set; }

    public virtual Crew Crew { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
