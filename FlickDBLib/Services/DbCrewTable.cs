using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FlickDBLib.Services
{
    public class DbCrewTable : DbTable, IDbCreateRecord, IDbDeleteRecord, IDbUpdateRecord
    {
        private readonly IDbContextFactory<MovieContext> _dbfactory;

        public Movie? MovieCrew { get; set; }

        public List<Crew> Crews { get; set; } = new List<Crew>();

        public Crew? Crew { get; set; }

        public DbCrewTable(IDbContextFactory<MovieContext> dbfactory) : base(dbfactory)
        {
            _dbfactory = dbfactory;
        }

        public override async Task<IEnumerable<Crew>> ReadAllRecord(params object[] args)
        {
            if (args.Length == 1)
            {
                int movieid = (int)args[0];

                using (var db = _dbfactory.CreateDbContext())
                {
                
                    if (db.Movies != null)
                    {
                        MovieCrew = await db.Movies.Include(mc => mc.Moviescrews).ThenInclude(c => c.Crew).FirstOrDefaultAsync(m => m.Movieid == movieid);
                    }

                    if (MovieCrew != null)
                    Crews = await Task.FromResult(MovieCrew.Moviescrews.Select(a => a.Crew).ToList());

                    db.Dispose();
                    return Crews;
                }
            }
            else
            {
                return Crews;
            }
        }

        public override async Task<Crew?> ReadSingleRecord(params Object[] args)
        {
            if (args.Length == 2)
            {
                int movieid = (int)args[0];
                int crewid = (int)args[1];

                using (var db = _dbfactory.CreateDbContext())
                {
                    if (db.Movies != null)
                        MovieCrew = await db.Movies.Include(mc => mc.Moviescrews).ThenInclude(c => c.Crew).FirstOrDefaultAsync(m => m.Movieid == movieid);
                
                    if (MovieCrew != null)
                        Crew = await Task.FromResult(MovieCrew.Moviescrews.Select(c => c.Crew).FirstOrDefault(c => c.Crewid == crewid));

                    db.Dispose();
                    return Crew;
                }
            }
            else
            {
                return Crew;
            }
        }

        public async Task<bool> CreateRecord(params object[] args)
        {
            if (args.Length == 2)
            {
                CrewForm crewform = (CrewForm)args[0];
                int movieid = (int)args[1];

                var crew = new Crew
                {
                    Firstname = crewform.Firstname,
                    Lastname = crewform.Lastname,
                    Birth = crewform.Birth,
                    Biography = crewform.Biography,
                    Picture = crewform.GetPictureFileName(crewform.Firstname, crewform.Lastname),
                    Position = crewform.Position
                };

                using (var db = _dbfactory.CreateDbContext())
                {
                    if (db.Movies != null)
                    {
                        Movie? movie = await db.Movies.FindAsync(movieid);

                        if (movie != null)
                        {
                            MovieCrew? MovieCrew = new MovieCrew { Movie = movie, Crew = crew };
                            db.Moviescrews?.Add(MovieCrew);
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
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteRecord(int crewid)
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

        public async Task<bool> UpdateRecord(params object[] args)
        {
            if (args.Length == 2)
            {
                CrewForm crewform = (CrewForm)args[0];
                int crewid = (int)args[1];

                using (var db = _dbfactory.CreateDbContext())
                {
                    if (db.Crews != null && db.Moviescrews != null)
                    {
                        var crew = await db.Crews.FindAsync(crewid);
                        var moviecrew = await db.Moviescrews.FirstOrDefaultAsync(mc => mc.Crewid == crewid);

                        if (crew != null && moviecrew != null)
                        {
                            crew.Firstname = crewform.Firstname;
                            crew.Lastname = crewform.Lastname;
                            crew.Birth = crewform.Birth;
                            crew.Biography = crewform.Biography;
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
            else
            {
                return false;
            }
        }
    }
}