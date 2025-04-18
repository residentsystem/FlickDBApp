using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FlickDBLib.Services
{
    public class DbGenreTable : DbTable, IDbCreateRecord, IDbDeleteRecord, IDbUpdateRecord
    {
        private readonly IDbContextFactory<MovieContext> _dbfactory;

        public Movie? MovieGenre { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();

        public Genre? Genre { get; set; }

        public DbGenreTable(IDbContextFactory<MovieContext> dbfactory) : base(dbfactory)
        {
            _dbfactory = dbfactory;
        }

        public override async Task<IEnumerable<Genre>> ReadAllRecord(params object[] args)
        {
            if (args.Length == 1)
            {
                int movieid = (int)args[0];

                using (var db = _dbfactory.CreateDbContext())
                {
                    try {
                        if (db.Movies != null)
                        {
                            MovieGenre = await db.Movies.Include(mg => mg.Moviesgenres).ThenInclude(g => g.Genre).FirstOrDefaultAsync(m => m.Movieid == movieid);
                        }

                        if (MovieGenre != null)
                        Genres = await Task.FromResult(MovieGenre.Moviesgenres.Select(g => g.Genre).ToList());

                        db.Dispose();
                        return Genres;
                    } catch (Exception) {
                        throw new FindArgumentNullException();
                    }
                }
            }
            else
            {
                return Genres;
            }
        }

        public override async Task<Genre?> ReadSingleRecord(params object[] args)
        {
            if (args.Length == 2)
            {
                int movieid = (int)args[0];
                int genreid = (int)args[1];

                using (var db = _dbfactory.CreateDbContext())
                {
                    try {
                        if (db.Movies != null)
                            MovieGenre = await db.Movies.Include(mg => mg.Moviesgenres).ThenInclude(g => g.Genre).FirstOrDefaultAsync(m => m.Movieid == movieid);
                        
                        if (MovieGenre != null)
                            Genre = await Task.FromResult(MovieGenre.Moviesgenres.Select(g => g.Genre).FirstOrDefault(g => g.Genreid == genreid));

                        db.Dispose();
                        return Genre;
                    } catch (Exception) {
                        throw new FindArgumentNullException();
                    }
                }
            }
            else
            {
                return Genre;
            }
        }

        public async Task<bool> CreateRecord(params object[] args)
        {
            if (args.Length == 2)
            {
                GenreForm genreform = (GenreForm)args[0];
                int movieid = (int)args[1];

                var genre = new Genre
                {
                    Genre1 = genreform.Genre1
                };

                // Read all genres from the database for the given movie
                IEnumerable<Genre> genres = await ReadAllRecord(movieid);

                // Check if genre already exists in the database
                // Note: This check is done against the list of genres for the movie, not the entire database
                var existingGenre = genres.FirstOrDefault(g => 
                    g.Genre1 == genreform.Genre1);

                if (existingGenre != null)
                {
                    // Genre already exists, return false or handle as needed
                    throw new CreateArgumentNullException("Genre already exists.");
                }

                using (var db = _dbfactory.CreateDbContext())
                {
                    try {
                        if (db.Movies != null)
                        {
                            Movie? movie = await db.Movies.FindAsync(movieid);

                            if (movie != null)
                            {
                                var MovieGenre = new MovieGenre { Movie = movie, Genre = genre };
                                db.Moviesgenres?.Add(MovieGenre);
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
                    } catch (Exception) {
                        throw new CreateArgumentNullException();
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteRecord(int genreid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                try {
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
                } catch (Exception) {
                    throw new DeleteArgumentNullException();
                }
            }
        }

        public async Task<bool> UpdateRecord(params object[] args)
        {
            if (args.Length == 2)
            {
                GenreForm genreform = (GenreForm)args[0];
                int genreid = (int)args[1];

                var db = _dbfactory.CreateDbContext();
                var currentgenre = await db.Genres.Include(g => g.Moviesgenres).FirstOrDefaultAsync(g => g.Genreid == genreid);

                int movieid = currentgenre.Moviesgenres.FirstOrDefault().Movieid;

                // Read all genres from the database for the given movie
                IEnumerable<Genre> genres = await ReadAllRecord(movieid);

                // Check if genre already exists in the database
                // Note: This check is done against the list of genres for the movie, not the entire database
                var existingGenre = genres.FirstOrDefault(g => 
                    g.Genre1 == genreform.Genre1);

                if (existingGenre != null)
                {
                    // Genre already exists, return false or handle as needed
                    throw new CreateArgumentNullException("Genre already exists.");
                }

                try {
                    if (db.Genres != null && db.Moviesgenres != null)
                    {
                        var genre = await db.Genres.FindAsync(genreid);
                        var moviegenre = await db.Moviesgenres.FirstOrDefaultAsync(mg => mg.Genreid == genreid);

                        if (genre != null && moviegenre != null)
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
                catch (Exception) {
                    throw new UpdateArgumentNullException();
                }
            }
            else
            {
                return false;
            }
        }
    }
}