namespace FlickDBLib.Database
{
    public interface IDatabaseConnection
    {
        string GetConnectionString(DatabaseEnvironment connectionstring);
        
        string MySqlConnectionStatus(string connectionstring);
    }
}