namespace FlickDB.Pages.Error.Production
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public string PageTitle = "Resident Bookmark - Error";

        private IDbConnection _database;

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string? Referrer { get; set; }

        public bool ShowReferrer => !string.IsNullOrEmpty(Referrer); 

        public string? ExceptionMessage { get; set; }

        public bool ShowExceptionMessage => !string.IsNullOrEmpty(ExceptionMessage);

        public string? MySqlExeptionMessage { get; set; }
        
        public bool ShowMySqlExeptionMessage => !string.IsNullOrEmpty(MySqlExeptionMessage);

        public ErrorModel(IDbConnection database)
        {
            _database = database;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            MySqlExeptionMessage = _database.MySqlConnectionStatus(_database.GetConnectionString());

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is FindMovieNullException)
            {
                ExceptionMessage = "Could not FIND any movie. Verify the movie still exist and try again.";
            }

            if (exceptionHandlerPathFeature?.Error is FindArgumentNullException)
            {
                ExceptionMessage = "Could not FIND any item. Verify the item still exist and try again.";
            }

            if (exceptionHandlerPathFeature?.Error is FindInvalidOperationException)
            {
                ExceptionMessage = "Could not FIND any item. Verify the item still exist and try again.";
            }

            if (exceptionHandlerPathFeature?.Error is DeleteArgumentNullException)
            {
                ExceptionMessage = "Could not DELETE this item. Verify the item still exist and try again.";
            }

            if (exceptionHandlerPathFeature?.Error is EditArgumentNullException)
            {
                ExceptionMessage = "Could not EDIT this item. Verify the item still exist and try again.";
            }

            if (exceptionHandlerPathFeature?.Error is EditNullReferenceException)
            {
                ExceptionMessage = "Could not EDIT this item. Verify the item still exist and try again.";
            }

            if (exceptionHandlerPathFeature?.Error is SettingFileNullReferenceException)
            {
                ExceptionMessage = "Configuration file was not found!!";
            }
        }

        public void OnPost()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            MySqlExeptionMessage = _database.MySqlConnectionStatus(_database.GetConnectionString());

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>(); 

            if (exceptionHandlerPathFeature?.Error is DeleteArgumentNullException)
            {
                ExceptionMessage = "Could not DELETE this item. Verify the item still exist and try again.";
            }

            if (exceptionHandlerPathFeature?.Error is DeleteInvalidOperationException)
            {
                ExceptionMessage = "Could not DELETE this item. Verify the item still exist and try again.";
            }

            if (exceptionHandlerPathFeature?.Error is DbUpdateConcurrencyException)
            {
                ExceptionMessage = "Could not UPDATE this item. Verify that the item still exist and try again.";
            }

            if (exceptionHandlerPathFeature?.Error is SettingFileNullReferenceException)
            {
                ExceptionMessage = "Configuration file was not found!!";
            }
        }
    }
}