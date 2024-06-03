namespace FlickDBLib.Interface
{
    public interface ICrewManager
    {
        Task<IEnumerable<Crew>> ReadAllCrew(int movieid);

        Task<Crew?> ReadCrew(int movieid, int crewid);

        Task<bool> AddCrew(CrewForm crewform, int movieid);

        Task<bool> RemoveCrew(int crewid);

        Task<bool> UpdateCrew(CrewForm crewform, int crewid);
    }
}