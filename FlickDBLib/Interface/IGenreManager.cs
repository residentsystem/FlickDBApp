namespace FlickDBLib.Interface
{
    public interface IGenreManager
    {
        Task<IEnumerable<Genre>> ReadAllGenre(int movieid);

        Task<Genre?> ReadGenre(int movieid, int genreid);

        Task<bool> AddGenre(GenreForm genreform, int movieid);

        Task<bool> RemoveGenre(int genreid);

        Task<bool> UpdateGenre(GenreForm genreform, int genreid);
    }
}