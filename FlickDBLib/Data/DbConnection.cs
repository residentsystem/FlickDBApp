namespace FlickDBLib.Data
{
    public class DbConnection : IDbConnection
    {
        private IWebHostEnvironment _environment;

        private MySqlConnection? connection;
        
        public string? Message { get; set; }

        public DbConnection(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // Retrieve connection string from environment variable

        public string? GetConnectionString()
        {
            if (_environment.IsDevelopment()) {
                return Environment.GetEnvironmentVariable("CONNSTR_MOVIE_DEV");
            }
            else if (_environment.IsStaging()) {
                return Environment.GetEnvironmentVariable("CONNSTR_MOVIE_STG");
            }
            else {
                return Environment.GetEnvironmentVariable("CONNSTR_MOVIE_PRD");
            }
        }

        public string? MySqlConnectionStatus(string? connectionstring)
        {
            try
            {
                connection = new MySqlConnection(connectionstring);
                connection.Open();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Message = $"MySql Error {ex.Number}: Cannot connect to the server. Contact your administrator.";
                        break;
                    case 1042:
                        Message = $"MySql Error {ex.Number}: Unable to connect to any of the specified MySQL hosts. Contact your administrator.";
                        break;
                    case 1045:
                        Message = $"MySql Error {ex.Number}: Invalid username/password, please try again.";
                        break;
                    default:
                        Message = $"MySql Error {ex.Number}: Unknown error when trying to connect to the server. Contact your administrator.";
                        break;
                }
            }
            return Message;
        }
    }
}