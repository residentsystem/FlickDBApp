using System.Reflection.Metadata.Ecma335;

public class MovieForm
{
    [Required (ErrorMessage = "The Title field is required.")]
    public string Title { get; set; } = string.Empty;

    [Required (ErrorMessage = "The Format field is required.")]
    public string Format { get; set; } = string.Empty;

    public int DurationHours { get; set; }

    public int DurationMinutes { get; set; }

    public TimeSpan Duration => new TimeSpan(DurationHours, DurationMinutes, 0);

    public DateOnly Release { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required (ErrorMessage = "The Rating field is required.")]
    public string SelectedRating { get; set; } = string.Empty;

    public string Rating { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;

    public string Poster { get; set; } = string.Empty;

    [Required (ErrorMessage = "The Story field is required.")]
    public string Story { get; set; } = string.Empty;

    public string GetRatingDescription() 
    {
        if (SelectedRating == "General")
            return "General";
            
        else if (SelectedRating == "13 years and over")
            return "13 years and over";

        else if (SelectedRating == "16 years and over")
            return "16 years and over";
        
        else if (SelectedRating == "18 years and over")
            return "18 years and over";

        else
            return "None";
    }

    public string GetRatingSymbol() 
    {
        if (SelectedRating == "General")
            return "RatingG.png";

        else if (SelectedRating == "13 years and over")
            return "Rating13.png";

        else if (SelectedRating == "16 years and over")
            return "Rating16.png";

        else if (SelectedRating == "18 years and over")
            return "Rating18.png";

        else
            return "";
    }

    public string GetPosterFileName(string title)
    {
        return title.Replace(" ", "-") + ".jpg";
    }
}