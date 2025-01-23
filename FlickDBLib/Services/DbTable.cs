namespace FlickDBLib.Services
{
    public abstract class DbTable
    {
        private readonly IDbContextFactory<MovieContext> _dbfactory;

        public DbTable(IDbContextFactory<MovieContext> dbfactory)
        {
            _dbfactory = dbfactory;
        }

        public abstract Task ReadAllRecord(params object[] args);

        public abstract Task ReadSingleRecord(params object[] args);

        public async Task<IEnumerable<Movie>> ReadRecordByGenre(string genre)
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
    }
}