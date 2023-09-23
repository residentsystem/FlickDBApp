namespace FlickDB.Services.Setting;

public class SettingService : ISettingService
{
    public SettingService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private readonly IConfiguration _configuration;

    public MailSettings GetMailSettings()
    {
        MailSettings? settings = _configuration.GetSection("Mail").Get<MailSettings>();

        // Throw an exception when an option is null or empty.
        if (settings == null)
        {
            throw new ArgumentNullException();
        }
        else if (string.IsNullOrEmpty(settings.Name))
        {
            throw new ArgumentNullException();
        }
        else if (string.IsNullOrEmpty(settings.Address))
        {
            throw new ArgumentNullException();
        }
        else if (string.IsNullOrEmpty(settings.Host))
        {
            throw new ArgumentNullException();
        }
        else if (settings.Port == 0)
        {
            throw new ArgumentNullException();
        }
        else if (string.IsNullOrEmpty(settings.Username))
        {
            throw new ArgumentNullException();
        }
        else if (string.IsNullOrEmpty(settings.Password))
        {
            throw new ArgumentNullException();
        }
        else if (string.IsNullOrEmpty(settings.SenderName))
        {
            throw new ArgumentNullException();
        }
        else if (string.IsNullOrEmpty(settings.SenderAddress))
        {
            throw new ArgumentNullException();
        }
        else if (string.IsNullOrEmpty(settings.Subject))
        {
            throw new ArgumentNullException();
        }
        else
        {
            return settings;
        }
    }
}