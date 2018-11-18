using MongoDB.Driver;
using PropertyTracker.Data.Repository.Interfaces;
using PropertyTracker.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository
{
    public class SiteRepository : GenericRepository<Site>, ISiteRepository
    {
        public SiteRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public Task<List<Site>> GetAllAsync(bool? active)
        {
            return this.GetAllAsync<Site>(s => !active.HasValue || s.Active == active.Value);
        }
    }
}
