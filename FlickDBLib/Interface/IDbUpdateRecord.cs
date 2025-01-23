namespace FlickDBLib.Interface
{
    public interface IDbUpdateRecord
    {
        Task<bool> UpdateRecord(params object[] args);
    }
}