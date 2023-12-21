using System;
using System.Collections.Generic;

namespace FlickDB.Models;

public partial class Movie
{
    public int Movieid { get; set; }

    public string? Title { get; set; }

    public string? Format { get; set; }

    public TimeSpan? Duration { get; set; }

    public DateOnly? Release { get; set; }

    public string? Rating { get; set; }

    public string? Symbol { get; set; }

    public string? Poster { get; set; }

    public string? Story { get; set; }

    public virtual ICollection<Moviesactor> Moviesactors { get; } = new List<Moviesactor>();

    public virtual ICollection<Moviescrew> Moviescrews { get; } = new List<Moviescrew>();

    public virtual ICollection<Moviesgenre> Moviesgenres { get; } = new List<Moviesgenre>();
}
