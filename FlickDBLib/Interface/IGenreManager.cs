namespace FlickDBLib.Interface
{
    public interface IGenreManager
    {
        public Movie? MovieGenre { get; set; }

        public List<Genre> Genres { get; set; }

        public Genre? Genre { get; set; }

        Task<IEnumerable<Genre>> ReadAllGenre(int movieid);

        Task<Genre?> ReadGenre(int movieid, int genreid);

        Task<bool> AddGenre(GenreForm genreform, int movieid);

        Task<bool> RemoveGenre(int genreid);

        Task<bool> UpdateGenre(GenreForm genreform, int genreid);
    }
}