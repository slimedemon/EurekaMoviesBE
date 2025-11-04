namespace EurekaMoviesBE.Entities.Tmdb;

public class People : TmdbBase
{
    [BsonElement("adult")]
    public bool Adult { get; set; }

    [BsonElement("also_known_as")]
    public List<string> AlsoKnownAs { get; set; } = default!;

    [BsonElement("biography")]
    public string Biography { get; set; } = default!;

    [BsonElement("birthday")]
    public string Birthday { get; set; } = default!;

    [BsonElement("deathday")]
    public string Deathday { get; set; } = default!;

    [BsonElement("gender")]
    public int Gender { get; set; } = default!;

    [BsonElement("homepage")]
    public string Homepage { get; set; } = default!;

    [BsonElement("id")]
    public int _TmdbId { get; set; }

    [BsonElement("imdb_id")]
    public string ImdbId { get; set; } = default!;

    [BsonElement("known_for_department")]
    public string KnownForDepartment { get; set; } = default!;

    [BsonElement("movie_credits")]
    public MovieCredit MovieCredits { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;

    [BsonElement("place_of_birth")]
    public string PlaceOfBirth { get; set; } = default!;

    [BsonElement("popularity")]
    public double Popularity { get; set; }

    [BsonElement("profile_path")]
    public string ProfilePath { get; set; } = default!;
}

public class MovieCredit
{
    [BsonElement("cast")]
    public List<MovieCast> Cast { get; set; } = default!;

    [BsonElement("crew")]
    public List<MovieCrew> Crew { get; set; } = default!;

    [BsonElement("id")]
    public int IdNumber { get; set; }
}

public class MovieCast
{
    [BsonElement("adult")]
    public bool Adult { get; set; }

    [BsonElement("backdrop_path")]
    public string BackdropPath { get; set; } = default!;

    [BsonElement("genre_ids")]
    public List<int> GenreIds { get; set; } = default!;

    [BsonElement("id")]
    public int IdNumber { get; set; }

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

    [BsonElement("credit_id")]
    public string CreditId { get; set; } = default!;

    [BsonElement("department")]
    public string Department { get; set; } = default!;

    [BsonElement("job")]
    public string Job { get; set; } = default!;

    [BsonElement("character")]
    public string Character { get; set; } = default!;

    [BsonElement("order")]
    public int Order { get; set; }
}

public class MovieCrew
{
    [BsonElement("adult")]
    public bool Adult { get; set; }

    [BsonElement("backdrop_path")]
    public string BackdropPath { get; set; } = default!;

    [BsonElement("genre_ids")]
    public List<int> GenreIds { get; set; } = default!;

    [BsonElement("id")]
    public int IdNumber { get; set; }

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

    [BsonElement("credit_id")]
    public string CreditId { get; set; } = default!;

    [BsonElement("department")]
    public string Department { get; set; } = default!;

    [BsonElement("job")]
    public string Job { get; set; } = default!;
}