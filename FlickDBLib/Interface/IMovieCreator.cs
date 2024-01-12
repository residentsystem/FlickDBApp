namespace FlickDBLib.Interface
{
    public interface IMovieCreator
    {
        Task<bool> AddMovie(MovieForm movieform);
    }
}