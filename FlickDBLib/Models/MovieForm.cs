using System.Reflection.Metadata.Ecma335;

public class MovieForm
{
    [Required (ErrorMessage = "Movie title is required")]
    public string Title { get; set; } = string.Empty;

    [Required (ErrorMessage = "Movie format is required")]
    public string Format { get; set; } = string.Empty;

    [Required (ErrorMessage = "Duration in hour(s) is required")]
    public int DurationHours { get; set; }

    [Required (ErrorMessage = "Duration in minute(s) is required")]
    public int DurationMinutes { get; set; }

    public TimeSpan Duration => new TimeSpan(DurationHours, DurationMinutes, 0);

    [Required (ErrorMessage = "Movie release date is required")]
    public DateOnly Release { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required (ErrorMessage = "Movie rating is required")]
    public string SelectedRating { get; set; } = string.Empty;

    public string Rating { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;

    public string Poster { get; set; } = string.Empty;

    [Required (ErrorMessage = "Movie story is required")]
    public string Story { get; set; } = string.Empty;

    public string GetRatingDescription() 
    {
        if (SelectedRating == "G")
            return "G - General Rating";
            
        else if (SelectedRating == "13+")
            return "13+ - 13 years and over";

        else if (SelectedRating == "16+")
            return "16+ - 16 years and over";
        
        else if (SelectedRating == "18+")
            return "18+ - 18 years and over";

        else
            return "None";
    }

    public string GetRatingSymbol() 
    {
        if (SelectedRating == "G")
            return "RatingG.png";

        else if (SelectedRating == "13+")
            return "Rating13.png";

        else if (SelectedRating == "16+")
            return "Rating16.png";

        else if (SelectedRating == "18+")
            return "Rating18.png";

        else
            return "";
    }

    public string GetPosterFileName(string title)
    {
        return title.Replace(" ", "-") + ".jpg";
    }
}