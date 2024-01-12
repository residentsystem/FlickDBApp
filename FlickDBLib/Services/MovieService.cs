namespace FlickDBLib.Services
{
    public class MovieService : IMovieReader, IMovieCreator, IMovieRemover
    {
        private readonly IDbContextFactory<MovieContext> _dbfactory;

        public List<MoviePoster> Posters { get; set; } = new List<MoviePoster>();

        public MovieService(IDbContextFactory<MovieContext> dbfactory)
        {
            _dbfactory = dbfactory;
        }

        public async Task<IEnumerable<MoviePoster>> ReadAllMovies()
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                {
                    var movies = await db.Movies.Select(movie => new MoviePoster
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
                    return Enumerable.Empty<MoviePoster>();
                }
            }
        }

        public async Task<Movie> ReadMovie(int movieid)
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

        public async Task<bool> AddMovie(MovieForm movieform)
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
                    //db.Movies.Remove(movie);
                    db.Entry(movie).State = EntityState.Deleted;
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
    }
}