namespace EurekaMovieBE.Entities.Tmdb;

public class Similar : TmdbBase
{
    [BsonElement("similar_movies")]
    public List<SimilarMovie> SimilarMovies { get; set; } = default!;
}

public class SimilarMovie
{
    [BsonElement("adult")]
    public bool Adult { get; set; }

    [BsonElement("backdrop_path")]
    public string BackdropPath { get; set; } = default!;

    [BsonElement("genre_ids")]
    public List<int> GenreIds { get; set; } = default!;

    [BsonElement("id")]
    public int Id { get; set; }

    [BsonElement("original_language")]
    public string OriginalLanguage { get; set; } = default!;

    [BsonElement("original_title")]
    public string OriginalTitle { get; set; } = default!;

    [BsonElement("overview")]
    public string Overview { get; set; } = default!;

    [BsonElement("popularity")]
    public double Popularity { get; set; }

    [BsonElement("poster_path")]
    public string PosterPath { get; set; } = default!;

    [BsonElement("release_date")]
    public string ReleaseDate { get; set; } = default!;

    [BsonElement("title")]
    public string Title { get; set; } = default!;

    [BsonElement("video")]
    public bool Video { get; set; }

    [BsonElement("vote_average")]
    public double VoteAverage { get; set; }

    [BsonElement("vote_count")]
    public int VoteCount { get; set; }
}
