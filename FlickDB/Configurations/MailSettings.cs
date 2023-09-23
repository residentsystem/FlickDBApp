namespace FlickDB.Configurations;
public class MailSettings
{
    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Host { get; set; } = string.Empty;

    public int Port { get; set; } 

    public string Username { get; set; } = string.Empty;
        
    public string Password { get; set; } = string.Empty;

    public string SenderName { get; set; } = string.Empty;

    public string SenderAddress { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;
}