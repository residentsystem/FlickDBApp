namespace FlickDBLib.Data
{
    public interface IDbConnection
    {
        public string? Message { get; set; }
        
        string? GetConnectionString();
        
        string? MySqlConnectionStatus(string? connectionstring);
    }
}