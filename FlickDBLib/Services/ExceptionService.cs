namespace FlickDBLib.Services
{
    public class FindMovieNullException : ArgumentNullException
    {
        public FindMovieNullException (string message = "Could not FIND any movie. Verify that the movie still exist and try again.") : base (message)
        {

        }
    }
    
    public class FindArgumentNullException : ArgumentNullException
    {
        public FindArgumentNullException (string message = "Could not FIND this item. Verify that the item still exist and try again.") : base (message)
        {

        }
    }

    public class FindInvalidOperationException : InvalidOperationException
    {
        public FindInvalidOperationException (string message = "Could not FIND this item. Verify that the item still exist and try again.") : base (message)
        {

        }
    }

    public class CreateArgumentNullException : ArgumentNullException
    {
        public CreateArgumentNullException (string message = "Could not CREATE this item. Verify that the item still exist and try again.") : base (message)
        {

        }
    }

    public class CreateInvalidOperationException : InvalidOperationException
    {
        public CreateInvalidOperationException (string message = "Could not CREATE this item. Verify that the item still exist and try again.") : base (message)
        {

        }
    }

    public class DeleteArgumentNullException : ArgumentNullException
    {
        public DeleteArgumentNullException (string message = "Could not DELETE this item. Verify that the item still exist and try again.") : base (message)
        {

        }
    }

    public class DeleteInvalidOperationException : InvalidOperationException
    {
        public DeleteInvalidOperationException (string message = "Could not DELETE this item. Verify that the item still exist and try again.") : base (message)
        {

        }
    }

    public class EditArgumentNullException : ArgumentNullException
    {
        public EditArgumentNullException (string message = "Could not EDIT this item. Verify that the item still exist and try again.") : base (message)
        {

        }
    }

    public class EditNullReferenceException : NullReferenceException
    {
        public EditNullReferenceException (string message = "Could not EDIT this item. Verify that the item still exist and try again.") : base (message)
        {

        }
    }

    public class UpdateArgumentNullException : ArgumentNullException
    {
        public UpdateArgumentNullException (string message = "Could not UPDATE this item. Verify that the item still exist and try again.") : base (message)
        {

        }
    }

    public class UpdateNullReferenceException : NullReferenceException
    {
        public UpdateNullReferenceException (string message = "Could not UPDATE this item. Verify that the item still exist and try again.") : base (message)
        {

        }
    }

    public class SettingFileNullReferenceException : NullReferenceException
    {
        public SettingFileNullReferenceException (string message = "Configuration file was not found!!") : base (message)
        {

        }
    }
}