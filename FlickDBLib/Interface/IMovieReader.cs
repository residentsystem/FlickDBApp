namespace FlickDBLib.Interface
{
    public interface IMovieReader
    {
        Task<IEnumerable<MoviePoster>> ReadAllMovies();

        Task<Movie> ReadMovie(int movieid);
    }
}