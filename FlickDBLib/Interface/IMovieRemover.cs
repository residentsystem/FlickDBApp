namespace FlickDBLib.Interface
{
    public interface IMovieRemover
    {
        Task<bool> RemoveMovie(int movieid);
    }
}