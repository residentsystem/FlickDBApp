using System;
using System.Collections.Generic;

namespace FlickDB.Models;

public partial class Movieposter
{
    public int Movieid { get; set; }
    
    public string? Title { get; set; }

    public TimeSpan? Duration { get; set; }

    public string? Poster { get; set; }
}