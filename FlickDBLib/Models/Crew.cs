using System;
using System.Collections.Generic;

namespace FlickDBLib.Models;

public partial class Crew
{
    public int Crewid { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateOnly? Birth { get; set; }

    public string? Picture { get; set; }

    public string Position { get; set; } = null!;

    public virtual ICollection<MovieCrew> Moviescrews { get; } = new List<MovieCrew>();
}
