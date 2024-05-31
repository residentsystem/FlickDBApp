namespace FlickDBLib.Interface
{
    public interface IActorManager
    {
        Task<IEnumerable<Actor>> ReadAllActors(int movieid);

        Task<Actor?> ReadActor(int movieid, int actorid);

        Task<bool> AddActor(ActorForm actorform, int movieid);

        Task<bool> RemoveActor(int actorid);

        Task<bool> UpdateActor(ActorForm actorform, int actorid);
    }
}