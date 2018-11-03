using MongoDB.Driver;
using PropertyTracker.Data.Repository.Interfaces;
using PropertyTracker.Domain.Model;

namespace PropertyTracker.Data.Repository
{
    public class SiteRepository : GenericRepository<Site>, ISiteRepository
    {
        public SiteRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }
    }
}
