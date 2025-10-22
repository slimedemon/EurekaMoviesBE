using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace EurekaMovieBE.Entities.Tmdb;

public class Movie : TmdbBase
{
    [BsonElement("__v")]
    public int Version { get; set; }

    [BsonElement("adult")]
    public bool Adult { get; set; }

    [BsonElement("backdrop_path")]
    public string BackdropPath { get; set; } = default!;

    [BsonElement("belongs_to_collection")]
    public object BelongsToCollection { get; set; } = default!;

    [BsonElement("budget")]
    public int Budget { get; set; }

    [BsonElement("categories")]
    public List<string> Categories { get; set; } = default!;

    [BsonElement("credits")]
    public Credits Credits { get; set; } = default!;

    [BsonElement("genres")]
    public List<Genre> Genres { get; set; } = default!;

    [BsonElement("homepage")]
    public string Homepage { get; set; } = default!;

    [BsonElement("id")]
    public int IdNumber { get; set; } 

    [BsonElement("imdb_id")]
    public string ImdbId { get; set; } = default!;

    [BsonElement("origin_country")]
    public List<string> OriginCountry { get; set; } = default!;

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

    [BsonElement("production_companies")]
    public List<ProductionCompany> ProductionCompanies { get; set; } = default!;

    [BsonElement("production_countries")]
    public List<ProductionCountry> ProductionCountries { get; set; } = default!;

    [BsonElement("release_date")]
    [BsonSerializer(typeof(CustomDateTimeSerializer))]
    public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

    [BsonElement("revenue")]
    public int Revenue { get; set; }

    [BsonElement("runtime")]
    public int Runtime { get; set; }

    [BsonElement("similar_movies")]
    public List<object> SimilarMovies { get; set; } = default!;

    [BsonElement("spoken_languages")]
    public List<SpokenLanguage> SpokenLanguages { get; set; } = default!;

    [BsonElement("status")]
    public string Status { get; set; } = default!;

    [BsonElement("tagline")]
    public string Tagline { get; set; } = default!;

    [BsonElement("title")]
    public string Title { get; set; } = default!;

    [BsonElement("trailers")]
    public List<Trailer> Trailers { get; set; } = default!;

    [BsonElement("video")]
    public bool Video { get; set; } 

    [BsonElement("vote_average")]
    public double VoteAverage { get; set; } 

    [BsonElement("vote_count")]
    public int VoteCount { get; set; }
}

public class Credits
{
    [BsonElement("id")]
    public int IdNumber { get; set; }

    [BsonElement("cast")]
    public List<Cast> Cast { get; set; } = default!;

    [BsonElement("crew")]
    public List<Crew> Crew { get; set; } = default!;

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string CreditsId { get; set; } = default!;
}

public class Cast
{
    [BsonElement("adult")]
    public bool Adult { get; set; }

    [BsonElement("gender")]
    public int Gender { get; set; }

    [BsonElement("id")]
    public int IdNumber { get; set; }

    [BsonElement("known_for_department")]
    public string KnownForDepartment { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;

    [BsonElement("original_name")]
    public string OriginalName { get; set; } = default!;

    [BsonElement("popularity")]
    public double Popularity { get; set; }

    [BsonElement("profile_path")]
    public string ProfilePath { get; set; } = default!;

    [BsonElement("cast_id")]
    public int CastId { get; set; }

    [BsonElement("character")]
    public string Character { get; set; } = default!;

    [BsonElement("credit_id")]
    public string CreditId { get; set; } = default!;

    [BsonElement("order")]
    public int Order { get; set; }
}

public class Crew
{
    [BsonElement("adult")]
    public bool Adult { get; set; }

    [BsonElement("gender")]
    public int Gender { get; set; }

    [BsonElement("id")]
    public int IdNumber { get; set; }

    [BsonElement("known_for_department")]
    public string KnownForDepartment { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;

    [BsonElement("original_name")]
    public string OriginalName { get; set; } = default!;

    [BsonElement("popularity")]
    public double Popularity { get; set; }

    [BsonElement("profile_path")]
    public string ProfilePath { get; set; } = default!;

    [BsonElement("credit_id")]
    public string CreditId { get; set; } = default!;

    [BsonElement("department")]
    public string Department { get; set; } = default!;

    [BsonElement("job")]
    public string Job { get; set; } = default!;
}

public class Genre
{
    [BsonElement("id")]
    public int IdNumber { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = default!;
}

public class ProductionCompany
{
    [BsonElement("id")]
    public int IdNumber { get; set; }

    [BsonElement("logo_path")]
    public string LogoPath { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;

    [BsonElement("origin_country")]
    public string OriginCountry { get; set; } = default!;
}

public class ProductionCountry
{
    [BsonElement("iso_3166_1")]
    public string Iso31661 { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;
}

public class SpokenLanguage
{
    [BsonElement("english_name")]
    public string EnglishName { get; set; } = default!;

    [BsonElement("iso_639_1")]
    public string Iso6391 { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;
}

public class Trailer
{
    [BsonElement("iso_639_1")]
    public string Iso6391 { get; set; } = default!;

    [BsonElement("iso_3166_1")]
    public string Iso31661 { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;

    [BsonElement("key")]
    public string Key { get; set; } = default!;

    [BsonElement("site")]
    public string Site { get; set; } = default!;

    [BsonElement("size")]
    public int Size { get; set; }

    [BsonElement("type")]
    public string Type { get; set; } = default!;

    [BsonElement("official")]
    public bool Official { get; set; }

    [BsonElement("published_at")]
    [BsonSerializer(typeof(CustomDateTimeSerializer))]
    public DateTime PublishedAt { get; set; }

    [BsonElement("id")]
    public string IdString { get; set; } = default!;
}

public class CustomDateTimeSerializer : IBsonSerializer<DateTime>
{
    public Type ValueType => throw new NotImplementedException();

    public DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        if (DateTime.TryParse(value, out var result))
        {
            return result;
        }
        return DateTime.MinValue;
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
    {
        context.Writer.WriteString(value.ToString("yyyy-MM-dd"));
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        context.Writer.WriteString(value.ToString());
    }

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        if (DateTime.TryParse(value, out var result))
        {
            return result;
        }
        return DateTime.MinValue;
    }
}