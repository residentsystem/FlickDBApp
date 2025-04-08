using System.Reflection.Metadata.Ecma335;

public class GenreForm
{
    [Required (ErrorMessage = "The Genre field is required.")]
    public string Genre1 { get; set; } = string.Empty;
}