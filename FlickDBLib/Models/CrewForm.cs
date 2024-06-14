using System.Reflection.Metadata.Ecma335;

public class CrewForm
{
    [Required (ErrorMessage = "Crew first name is required")]
    public string Firstname { get; set; } = string.Empty;

    [Required (ErrorMessage = "Crew last name is required")]
    public string Lastname { get; set; } = string.Empty;

    [Required (ErrorMessage = "Crew birth date is required")]
    public DateOnly Birth { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public string? Biography { get; set; }

    public string Picture { get; set; } = string.Empty;

    [Required (ErrorMessage = "Position is required")]
    public string Position { get; set; } = string.Empty;

    public string GetPictureFileName(string firstname, string lastname)
    {
        string fullname = firstname + " " + lastname;
        return fullname.Replace(" ", "") + ".jpg";
    }
}