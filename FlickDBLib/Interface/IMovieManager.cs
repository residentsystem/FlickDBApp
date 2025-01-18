namespace FlickDBLib.Interface
{
    public interface IMovieManager
    {
        public List<Movie> Posters { get; set; }

        public int Success { get; set; }

        Task<IEnumerable<Movie>> ReadAllMovie();

        Task<Movie?> ReadMovie(int movieid);

        Task<IEnumerable<Movie>> ReadMoviesByGenre(string genre);

        Task<bool> NewMovie(MovieForm movieform);

        Task<bool> RemoveMovie(int movieid);

        Task<bool> UpdateMovie(MovieForm movieform, int movieid);
    }
}