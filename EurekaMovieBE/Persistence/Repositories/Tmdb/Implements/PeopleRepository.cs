using EurekaMovieBE.Data;
using EurekaMovieBE.Entities.Tmdb;
using EurekaMovieBE.Persistence.Repositories.Tmdb.Interfaces;

namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class PeopleRepository : MongoGenericRepository<People>, IPeopleRepository
    {
        public PeopleRepository(TmdbDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}
