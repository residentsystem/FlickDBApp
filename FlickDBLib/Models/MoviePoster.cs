using System;
using System.Collections.Generic;

namespace FlickDBLib.Models;

public partial class MoviePoster
{
    public int Movieid { get; set; }
    
    public string? Title { get; set; }

    public TimeSpan? Duration { get; set; }

    public string? Poster { get; set; }
}