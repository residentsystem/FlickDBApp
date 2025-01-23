namespace FlickDBLib.Interface
{
    public interface IDbDeleteRecord
    {
        Task<bool> DeleteRecord(int id);
    }
}