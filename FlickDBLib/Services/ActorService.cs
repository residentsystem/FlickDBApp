namespace FlickDBLib.Services
{
    public class ActorService : IActorManager
    {
        private readonly IDbContextFactory<MovieContext> _dbfactory;

        public Movie? MovieActor { get; set; }

        public List<Actor> Actors { get; set; } = new List<Actor>();

        public Actor? Actor { get; set; }

        public ActorService(IDbContextFactory<MovieContext> dbfactory)
        {
            _dbfactory = dbfactory;
        }

        public async Task<IEnumerable<Actor>> ReadAllActor(int MovieId)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                
                if (db.Movies != null)
                {
                    MovieActor = await db.Movies.Include(ma => ma.Moviesactors).ThenInclude(a => a.Actor).FirstOrDefaultAsync(m => m.Movieid == MovieId);
                }

                if (MovieActor != null)
                Actors = await Task.FromResult(MovieActor.Moviesactors.Select(a => a.Actor).ToList());

                db.Dispose();
                return Actors;
    
            }
        }

        public async Task<Actor?> ReadActor(int MovieId, int ActorId)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                    MovieActor = await db.Movies.Include(ma => ma.Moviesactors).ThenInclude(a => a.Actor).FirstOrDefaultAsync(m => m.Movieid == MovieId);
                
                if (MovieActor != null)
                    Actor = await Task.FromResult(MovieActor.Moviesactors.Select(a => a.Actor).FirstOrDefault(a => a.Actorid == ActorId));

                db.Dispose();
                return Actor;

            }
        }

        public async Task<bool> AddActor(ActorForm actorform, int movieid)
        {
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
                if (db.Movies != null)
                {
                    Movie movie = await db.Movies.FindAsync(movieid);
                    var MovieActor = new MovieActor { Movie = movie, Actor = actor, Character = actorform.Character };

                    db.Moviesactors.Add(MovieActor);
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

        public async Task<bool> RemoveActor(int actorid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
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
            }
        }

        public async Task<bool> UpdateActor(ActorForm actorform, int actorid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Actors != null)
                {
                    var actor = await db.Actors.FindAsync(actorid);
                    var movieactor = await db.Moviesactors.FirstOrDefaultAsync(ma => ma.Actorid == actorid);

                    if (actor != null || movieactor != null)
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
            }
        }
    }
}