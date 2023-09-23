namespace FlickDBLib.Mail;

public class Mail
{
    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;

    public string SenderName { get; set; } = string.Empty;
    
    public string SenderAddress { get; set; } = string.Empty;
    
    public string Subject { get; set; } = string.Empty;
    
    public string Groupname { get; set; } = string.Empty;

    public string GroupUID { get; set; } = string.Empty;

    public decimal Totalgroup { get; set; } 
    
    public string Host { get; set; } = string.Empty;
    
    public int Port { get; set; }
    
    public string Username { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
}