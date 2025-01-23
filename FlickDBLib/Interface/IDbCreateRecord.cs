namespace FlickDBLib.Interface
{
    public interface IDbCreateRecord
    {
        Task<bool> CreateRecord(params object[] args);
    }
}