namespace FlickDBLib.Services
{
    public class CrewService : ICrewManager
    {
        private readonly IDbContextFactory<MovieContext> _dbfactory;

        Movie? MovieCrew { get; set; }

        List<Crew> Crews { get; set; } = new List<Crew>();

        Crew? Crew { get; set; }

        public CrewService(IDbContextFactory<MovieContext> dbfactory)
        {
            _dbfactory = dbfactory;
        }

        public async Task<IEnumerable<Crew>> ReadAllCrew(int MovieId)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                
                if (db.Movies != null)
                {
                    MovieCrew = await db.Movies.Include(mc => mc.Moviescrews).ThenInclude(c => c.Crew).FirstOrDefaultAsync(m => m.Movieid == MovieId);
                }

                if (MovieCrew != null)
                Crews = await Task.FromResult(MovieCrew.Moviescrews.Select(a => a.Crew).ToList());

                db.Dispose();
                return Crews;
    
            }
        }

        public async Task<Crew?> ReadCrew(int MovieId, int CrewId)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                    MovieCrew = await db.Movies.Include(mc => mc.Moviescrews).ThenInclude(c => c.Crew).FirstOrDefaultAsync(m => m.Movieid == MovieId);
                
                if (MovieCrew != null)
                    Crew = await Task.FromResult(MovieCrew.Moviescrews.Select(c => c.Crew).FirstOrDefault(c => c.Crewid == CrewId));

                db.Dispose();
                return Crew;

            }
        }

        public async Task<bool> AddCrew(CrewForm crewform, int movieid)
        {
            var crew = new Crew
            {
                Firstname = crewform.Firstname,
                Lastname = crewform.Lastname,
                Birth = crewform.Birth,
                Picture = crewform.GetPictureFileName(crewform.Firstname, crewform.Lastname),
                Position = crewform.Position

            };

            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Movies != null)
                {
                    Movie movie = await db.Movies.FindAsync(movieid);
                    var MovieCrew = new MovieCrew { Movie = movie, Crew = crew };

                    db.Moviescrews.Add(MovieCrew);
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

        public async Task<bool> RemoveCrew(int crewid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Crews != null)
                {
                    var crew = await db.Crews.FindAsync(crewid);

                    if (crew != null)
                    db.Entry(crew).State = EntityState.Deleted;
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

        public async Task<bool> UpdateCrew(CrewForm crewform, int crewid)
        {
            using (var db = _dbfactory.CreateDbContext())
            {
                if (db.Crews != null)
                {
                    var crew = await db.Crews.FindAsync(crewid);
                    var moviecrew = await db.Moviescrews.FirstOrDefaultAsync(mc => mc.Crewid == crewid);

                    if (crew != null || moviecrew != null)
                    {
                        crew.Firstname = crewform.Firstname;
                        crew.Lastname = crewform.Lastname;
                        crew.Birth = crewform.Birth;
                        crew.Picture = crewform.Picture;
                        crew.Position = crewform.Position;

                        db.Entry(crew).State = EntityState.Modified;
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