namespace FlickDBLib.Interface
{
    public interface IDatabaseConnector
    {
        string GetConnectionString(DatabaseEnvironment connectionstring);
        
        string MySqlConnectionStatus(string connectionstring);
    }
}