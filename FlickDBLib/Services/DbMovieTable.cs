using System.Data;

namespace FlickDBLib.Services
{
    public class DbMovieTable : DbTable, IDbCreateRecord, IDbDeleteRecord, IDbUpdateRecord
    {
        private readonly IDbContextFactory<MovieContext> _dbfactory;

        public List<Movie> Posters { get; set; } = new List<Movie>();

        public int Success { get; set; }

        public DbMovieTable(IDbContextFactory<MovieContext> dbfactory) : base(dbfactory)
        {
            _dbfactory = dbfactory;
        }

        public override async Task<IEnumerable<Movie>> ReadAllRecord(params object[] args)
        {
            if (args.Length == 0)
            {
                using (var db = _dbfactory.CreateDbContext())
                {
                    try {
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
                    } catch (Exception) {
                        throw new FindMovieNullException();
                    }
                }
            }
            else
            {
                return Enumerable.Empty<Movie>();
            }
        }

        public override async Task<Movie?> ReadSingleRecord(params object[] args)
        {
            if (args.Length == 1)
            {
                int movieid = (int)args[0];
                
                using (var db = _dbfactory.CreateDbContext())
                {
                    try {
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
                    } catch (Exception) {
                        throw new FindMovieNullException();
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CreateRecord(params object[] args)
        {
            if (args.Length == 1)
            {
                MovieForm movieform = (MovieForm)args[0];

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
                    try {
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

        public async Task<bool> DeleteRecord(int movieid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                try {
                    if (db.Movies != null)
                    {
                        var movie = await db.Movies.FindAsync(movieid);

                        if (movie != null)
                        {
                            DbActorTable actortable = new DbActorTable(_dbfactory);
                            var actors = await actortable.ReadAllRecord(movieid);

                            foreach (var actor in actors)
                            {
                                await actortable.DeleteRecord(actor.Actorid);
                            }

                            DbCrewTable crewtable = new DbCrewTable(_dbfactory);
                            var crews = await crewtable.ReadAllRecord(movieid);

                            foreach (var crew in crews)
                            {
                                await crewtable.DeleteRecord(crew.Crewid);
                            }

                            //IGenreManager genreManager = new GenreService(_dbfactory);
                            DbGenreTable genretable = new DbGenreTable(_dbfactory);
                            var genres = await genretable.ReadAllRecord(movieid);

                            foreach (var genre in genres)
                            {
                                await genretable.DeleteRecord(genre.Genreid);
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
                } catch (Exception) {
                    throw new DeleteArgumentNullException();
                }
            }
        }

        public async Task<bool> UpdateRecord(params object[] args)
        {
            if (args.Length == 2)
            {
                MovieForm movieform = (MovieForm)args[0];
                int movieid = (int)args[1];

                using (var db = _dbfactory.CreateDbContext())
                {
                    try {
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
                    } catch (Exception) {
                        throw new UpdateArgumentNullException();
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}