namespace FlickDBLib.Services
{
    public class DbActorTable : DbTable, IDbCreateRecord, IDbDeleteRecord, IDbUpdateRecord
    {
        private readonly IDbContextFactory<MovieContext> _dbfactory;

        public List<Actor> Actors { get; set; } = new List<Actor>();

        public Movie? MovieActor { get; set; }

        public Actor? Actor { get; set; }

        public DbActorTable(IDbContextFactory<MovieContext> dbfactory) : base(dbfactory)
        {
            _dbfactory = dbfactory;
        }

        public override async Task<IEnumerable<Actor>> ReadAllRecord(params object[] args)
        {
            if (args.Length == 1)
            {
                int movieid = (int)args[0];

                using (var db = _dbfactory.CreateDbContext())
                {
                    try {
                        if (db.Movies != null)
                        {
                            MovieActor = await db.Movies.Include(ma => ma.Moviesactors).ThenInclude(a => a.Actor).FirstOrDefaultAsync(m => m.Movieid == movieid);
                        }

                        if (MovieActor != null)
                        Actors = await Task.FromResult(MovieActor.Moviesactors.Select(a => a.Actor).ToList());

                        db.Dispose();
                        return Actors;
                    } catch (Exception) {
                        throw new FindArgumentNullException();
                    }
                }
            }
            else
            {
                return Actors;
            }
        }

        public override async Task<Actor?> ReadSingleRecord(params object[] args)
        {
            if (args.Length == 2)
            {
                int movieid = (int)args[0];
                int actorid = (int)args[1];

                using (var db = _dbfactory.CreateDbContext())
                {
                    try {
                        if (db.Movies != null)
                            MovieActor = await db.Movies.Include(ma => ma.Moviesactors).ThenInclude(a => a.Actor).FirstOrDefaultAsync(m => m.Movieid == movieid);
                    
                        if (MovieActor != null)
                            Actor = await Task.FromResult(MovieActor.Moviesactors.Select(a => a.Actor).FirstOrDefault(a => a.Actorid == actorid));

                        db.Dispose();
                        return Actor;
                    } catch (Exception) {
                        throw new FindArgumentNullException();
                    }
                }
            }
            else
            {
                return Actor;
            }
        }

        public async Task<bool> CreateRecord(params object[] args)
        {
            if (args.Length == 2)
            {
                ActorForm actorform = (ActorForm)args[0];
                int movieid = (int)args[1];

                var actor = new Actor
                {
                    Firstname = actorform.Firstname,
                    Lastname = actorform.Lastname,
                    Birth = actorform.Birth,
                    Biography = actorform.Biography,
                    Picture = actorform.GetPictureFileName(actorform.Firstname, actorform.Lastname)
                };

                using (var db = _dbfactory.CreateDbContext())
                {
                    try {
                        if (db.Movies != null)
                        {
                            Movie? movie = await db.Movies.FindAsync(movieid);

                            if (movie != null)
                            {
                                var MovieActor = new MovieActor { Movie = movie, Actor = actor, Character = actorform.Character };
                                db.Moviesactors?.Add(MovieActor);
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

        public async Task<bool> DeleteRecord(int actorid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                try {
                    if (db.Actors != null)
                    {
                        var actor = await db.Actors.FindAsync(actorid);

                        if (actor != null)
                        db.Entry(actor).State = EntityState.Deleted;
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
                ActorForm actorform = (ActorForm)args[0];
                int actorid = (int)args[1];

                using (var db = _dbfactory.CreateDbContext())
                {
                    try {
                        if (db.Actors != null && db.Moviesactors != null)
                        {
                            var actor = await db.Actors.FindAsync(actorid);
                            var movieactor = await db.Moviesactors.FirstOrDefaultAsync(ma => ma.Actorid == actorid);

                            if (actor != null && movieactor != null)
                            {
                                actor.Firstname = actorform.Firstname;
                                actor.Lastname = actorform.Lastname;
                                actor.Birth = actorform.Birth;
                                actor.Biography = actorform.Biography;
                                actor.Picture = actorform.Picture;
                                movieactor.Character = actorform.Character;

                                db.Entry(actor).State = EntityState.Modified;
                                db.Entry(movieactor).State = EntityState.Modified;
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