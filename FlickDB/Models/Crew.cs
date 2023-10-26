using System;
using System.Collections.Generic;

namespace FlickDB.Models;

public partial class Crew
{
    public int Crewid { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateOnly? Birth { get; set; }

    public string Position { get; set; } = null!;

    public string? Picture { get; set; }

    public virtual ICollection<Moviescrew> Moviescrews { get; } = new List<Moviescrew>();
}
