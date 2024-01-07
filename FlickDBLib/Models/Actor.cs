using System;
using System.Collections.Generic;

namespace FlickDBLib.Models;

public partial class Actor
{
    public int Actorid { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateOnly? Birth { get; set; }

    public string? Picture { get; set; }

    public virtual ICollection<MovieActor> Moviesactors { get; } = new List<MovieActor>();
}
