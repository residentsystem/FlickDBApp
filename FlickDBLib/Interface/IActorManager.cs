namespace FlickDBLib.Interface
{
    public interface IActorManager
    {
        public Movie? MovieActor { get; set; }

        public List<Actor> Actors { get; set; }

        public Actor? Actor { get; set; }

        Task<IEnumerable<Actor>> ReadAllActor(int movieid);

        Task<Actor?> ReadActor(int movieid, int actorid);

        Task<bool> NewActor(ActorForm actorform, int movieid);

        Task<bool> RemoveActor(int actorid);

        Task<bool> UpdateActor(ActorForm actorform, int actorid);
    }
}