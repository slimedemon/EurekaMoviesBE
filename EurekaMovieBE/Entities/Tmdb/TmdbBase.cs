using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EurekaMovieBE.Entities.Tmdb
{
    public abstract class TmdbBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; } = default!;

        [BsonElement("tmdb_id")]
        public long TmdbId { get; set; }
    }
}
