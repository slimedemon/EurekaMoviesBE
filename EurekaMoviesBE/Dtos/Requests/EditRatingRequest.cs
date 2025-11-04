namespace EurekaMoviesBE.Dtos.Requests;

public class EditRatingRequest
{
    public double Stars { get; set; }
    public string Comment { get; set; }
    public long RatingId { get; set; }
}