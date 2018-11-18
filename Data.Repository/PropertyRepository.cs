using MongoDB.Driver;
using PropertyTracker.Data.Repository.Interfaces;
using PropertyTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public Task<List<Property>> GetAllAsync(Guid searchId)
        {
            return this.GetAllAsync<Property>(p => p.SearchId.Equals(searchId));
        }
    }
}
