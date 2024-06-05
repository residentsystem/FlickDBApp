using System.Reflection.Metadata.Ecma335;

public class GenreForm
{
    [Required (ErrorMessage = "A genre is required")]
    public string Genre1 { get; set; } = string.Empty;
}