using System.Reflection.Metadata.Ecma335;

public class ActorForm
{
    [Required (ErrorMessage = "Actor first name is required")]
    public string Firstname { get; set; } = string.Empty;

    [Required (ErrorMessage = "Actor last name is required")]
    public string Lastname { get; set; } = string.Empty;

    [Required (ErrorMessage = "Actor birth date is required")]
    public DateOnly Birth { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public string Picture { get; set; } = string.Empty;

    [Required (ErrorMessage = "Character name is required")]
    public string Character { get; set; } = string.Empty;

    public string GetPictureFileName(string firstname, string lastname)
    {
        string fullname = firstname + " " + lastname;
        return fullname.Replace(" ", "") + ".jpg";
    }
}