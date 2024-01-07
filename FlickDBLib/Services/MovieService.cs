namespace FlickDBLib.Services
{
    public class MovieService : IMovieCreator
    {
        private readonly IDbContextFactory<MovieContext> _dbfactory;

        private readonly MovieForm _movieform;

        public MovieService(IDbContextFactory<MovieContext> dbfactory, MovieForm movieform)
        {
            _dbfactory = dbfactory;
            _movieform = movieform;
        }

        public async Task<bool> AddMovie()
        {
            var movie = new Movie
            {
                Title = _movieform.Title,
                Format = _movieform.Format,
                Duration = _movieform.Duration,
                Release = _movieform.Release,
                Rating = _movieform.GetRatingDescription(),
                Symbol = _movieform.GetRatingSymbol(),
                Poster = _movieform.GetPosterFileName(_movieform.Title),
                Story = _movieform.Story
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
    }
}