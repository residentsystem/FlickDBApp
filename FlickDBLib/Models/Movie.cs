using System;
using System.Collections.Generic;

namespace FlickDBLib.Models;

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

    public virtual ICollection<MovieActor> Moviesactors { get; } = new List<MovieActor>();

    public virtual ICollection<MovieCrew> Moviescrews { get; } = new List<MovieCrew>();

    public virtual ICollection<MovieGenre> Moviesgenres { get; } = new List<MovieGenre>();
}
