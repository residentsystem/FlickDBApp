namespace FlickDBLib.Interface
{
    public interface IMovieUpdator
    {
        Task<bool> UpdateMovie(MovieForm movieform, int movieid);
    }
}