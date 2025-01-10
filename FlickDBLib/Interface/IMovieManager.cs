namespace FlickDBLib.Interface
{
    public interface IMovieManager
    {
        public List<MoviePoster> Posters { get; set; }

        public int Changes { get; set; }

        Task<IEnumerable<MoviePoster>> ReadAllMovies();

        Task<Movie> ReadMovie(int movieid);

        Task<bool> AddMovie(MovieForm movieform);

        Task<bool> RemoveMovie(int movieid);

        Task<bool> UpdateMovie(MovieForm movieform, int movieid);
    }
}