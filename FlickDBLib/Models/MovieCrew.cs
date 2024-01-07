using System;
using System.Collections.Generic;

namespace FlickDBLib.Models;

public partial class MovieCrew
{
    public int Moviecrewid { get; set; }

    public int Movieid { get; set; }

    public int Crewid { get; set; }

    public virtual Crew Crew { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
