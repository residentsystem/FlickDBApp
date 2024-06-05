namespace FlickDBLib.Services
{
    public class GenreService : IGenreManager
    {
        private readonly IDbContextFactory<MovieContext> _dbfactory;

        Movie? MovieGenre { get; set; }

        List<Genre> Genres { get; set; } = new List<Genre>();

        Genre? Genre { get; set; }

        public GenreService(IDbContextFactory<MovieContext> dbfactory)
        {
            _dbfactory = dbfactory;
        }

        public async Task<IEnumerable<Genre>> ReadAllGenre(int MovieId)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                
                if (db.Movies != null)
                {
                    MovieGenre = await db.Movies.Include(mg => mg.Moviesgenres).ThenInclude(g => g.Genre).FirstOrDefaultAsync(m => m.Movieid == MovieId);
                }

                if (MovieGenre != null)
                Genres = await Task.FromResult(MovieGenre.Moviesgenres.Select(g => g.Genre).ToList());

                db.Dispose();
                return Genres;
    
            }
        }

        public async Task<Genre?> ReadGenre(int MovieId, int GenreId)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                    MovieGenre = await db.Movies.Include(mg => mg.Moviesgenres).ThenInclude(g => g.Genre).FirstOrDefaultAsync(m => m.Movieid == MovieId);
                
                if (MovieGenre != null)
                    Genre = await Task.FromResult(MovieGenre.Moviesgenres.Select(g => g.Genre).FirstOrDefault(g => g.Genreid == GenreId));

                db.Dispose();
                return Genre;

            }
        }

        public async Task<bool> AddGenre(GenreForm genreform, int movieid)
        {
            var genre = new Genre
            {
                Genre1 = genreform.Genre1
            };

            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                {
                    Movie movie = await db.Movies.FindAsync(movieid);
                    var MovieGenre = new MovieGenre { Movie = movie, Genre = genre };

                    db.Moviesgenres.Add(MovieGenre);
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

        public async Task<bool> RemoveGenre(int genreid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Genres != null)
                {
                    var genre = await db.Genres.FindAsync(genreid);

                    if (genre != null)
                    db.Entry(genre).State = EntityState.Deleted;
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

        public async Task<bool> UpdateGenre(GenreForm genreform, int genreid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Genres != null)
                {
                    var genre = await db.Genres.FindAsync(genreid);
                    var moviegenre = await db.Moviesgenres.FirstOrDefaultAsync(mg => mg.Genreid == genreid);

                    if (genre != null || moviegenre != null)
                    {
                        genre.Genre1 = genreform.Genre1;

                        db.Entry(genre).State = EntityState.Modified;
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