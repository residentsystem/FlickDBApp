namespace FlickDBLib.Database
{
    public interface IDatabaseConnection
    {
        string? GetConnectionString();
        
        string? MySqlConnectionStatus(string? connectionstring);
    }
}