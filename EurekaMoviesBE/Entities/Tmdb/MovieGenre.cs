namespace EurekaMoviesBE.Entities.Tmdb
{
    public class MovieGenre: TmdbBase
    {
        [BsonElement("name")]
        public string Name { get; set; } = default!;

        [BsonElement("id")]
        public int IdNumber { get; set; }
    }
}
