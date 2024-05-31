namespace FlickDBLib.Interface
{
    public interface IMovieManager
    {
        Task<IEnumerable<MoviePoster>> ReadAllMovies();

        Task<Movie> ReadMovie(int movieid);

        Task<bool> AddMovie(MovieForm movieform);

        Task<bool> RemoveMovie(int movieid);

        Task<bool> UpdateMovie(MovieForm movieform, int movieid);
    }
}