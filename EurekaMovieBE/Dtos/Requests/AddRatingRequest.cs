namespace EurekaMovieBE.Dtos.Requests;

public class AddRatingRequest
{
    public double Stars { get; set; }
    public string Comment { get; set; }
    public long TmdbId { get; set; } // movie id
}