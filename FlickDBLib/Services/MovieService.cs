namespace FlickDBLib.Services
{
    public class MovieService : IMovieManager
    {
        private readonly IDbContextFactory<MovieContext> _dbfactory;

        public List<Movie> Posters { get; set; } = new List<Movie>();

        public int Success { get; set; }

        public MovieService(IDbContextFactory<MovieContext> dbfactory)
        {
            _dbfactory = dbfactory;
        }

        public async Task<IEnumerable<Movie>> ReadAllMovie()
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                {
                    var movies = await db.Movies.Select(movie => new Movie
                    {
                        Movieid = movie.Movieid,
                        Title = movie.Title,
                        Poster = movie.Poster,
                        Duration = movie.Duration
                    })
                    .ToListAsync();
                    db.Dispose();

                    return movies;
                }
                else
                {
                    db.Dispose();
                    return Enumerable.Empty<Movie>();
                }
            }
        }

        public async Task<Movie?> ReadMovie(int movieid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                {
                    var movie = await db.Movies.FindAsync(movieid);
                    db.Dispose();

                    if (movie != null)
                    {
                        return movie;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    db.Dispose();
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Movie>> ReadMoviesByGenre(string genre)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                {
                    var movies = await db.Movies
                        .Where(movie => movie.Moviesgenres.Any(mg => mg.Genre.Genre1 == genre))
                        .Select(movie => new Movie
                        {
                            Movieid = movie.Movieid,
                            Title = movie.Title,
                            Poster = movie.Poster,
                            Duration = movie.Duration
                        })
                        .ToListAsync();
                    db.Dispose();

                    return movies;
                }
                else
                {
                    db.Dispose();
                    return Enumerable.Empty<Movie>();
                }
            }
        }

        public async Task<bool> NewMovie(MovieForm movieform)
        {
            var movie = new Movie
            {
                Title = movieform.Title,
                Format = movieform.Format,
                Duration = movieform.Duration,
                Release = movieform.Release,
                Rating = movieform.GetRatingDescription(),
                Symbol = movieform.GetRatingSymbol(),
                Poster = movieform.GetPosterFileName(movieform.Title),
                Story = movieform.Story
            };

            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                {
                    db.Movies.Add(movie);
                    int changes = await db.SaveChangesAsync();
                    db.Dispose();
                    return changes > 0;
                }
                else
                {
                    db.Dispose();
                    return false;
                }
            }
        }

        public async Task<bool> RemoveMovie(int movieid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                {
                    var movie = await db.Movies.FindAsync(movieid);

                    if (movie != null)
                    {
                        IActorManager actorManager = new ActorService(_dbfactory);
                        var actors = await actorManager.ReadAllActor(movieid);

                        foreach (var actor in actors)
                        {
                            await actorManager.RemoveActor(actor.Actorid);
                        }

                        ICrewManager crewManager = new CrewService(_dbfactory);
                        var crews = await crewManager.ReadAllCrew(movieid);

                        foreach (var crew in crews)
                        {
                            await crewManager.RemoveCrew(crew.Crewid);
                        }

                        IGenreManager genreManager = new GenreService(_dbfactory);
                        var genres = await genreManager.ReadAllGenre(movieid);

                        foreach (var genre in genres)
                        {
                            await genreManager.RemoveGenre(genre.Genreid);
                        }

                        db.Entry(movie).State = EntityState.Deleted;
                        Success = await db.SaveChangesAsync();
                        db.Dispose();
                    }
                    return Success > 0;
                }
                else
                {
                    db.Dispose();
                    return false;
                }
            }
        }

        public async Task<bool> UpdateMovie(MovieForm movieform, int movieid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                {
                    var movie = await db.Movies.FindAsync(movieid);

                    if (movie != null)
                    {
                        movie.Title = movieform.Title;
                        movie.Format = movieform.Format;
                        movie.Duration = movieform.Duration;
                        movie.Release = movieform.Release;
                        movie.Rating = movieform.GetRatingDescription();
                        movie.Symbol = movieform.GetRatingSymbol();
                        movie.Poster = movieform.Poster;
                        movie.Story = movieform.Story;

                        db.Entry(movie).State = EntityState.Modified;
                        int changes = await db.SaveChangesAsync();
                        db.Dispose();
                        return changes > 0;
                    }
                    else
                    {
                        db.Dispose();
                        return false;
                    }
                }
                else
                {
                    db.Dispose();
                    return false;
                }
            }
        }
    }
}