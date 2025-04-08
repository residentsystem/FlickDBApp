using System.Reflection.Metadata.Ecma335;

public class ActorForm
{
    [Required (ErrorMessage = "The Firstname field is required.")]
    public string Firstname { get; set; } = string.Empty;

    [Required (ErrorMessage = "The Lastname field is required.")]
    public string Lastname { get; set; } = string.Empty;

    public DateOnly Birth { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required (ErrorMessage = "The Biography field is required.")]
    public string? Biography { get; set; }

    public string Picture { get; set; } = string.Empty;

    [Required (ErrorMessage = "The Character field is required.")]
    public string Character { get; set; } = string.Empty;

    public string GetPictureFileName(string firstname, string lastname)
    {
        string fullname = firstname + " " + lastname;
        return fullname.Replace(" ", "") + ".jpg";
    }
}