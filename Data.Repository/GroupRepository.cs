using MongoDB.Driver;
using PropertyTracker.Data.Repository.Interfaces;
using PropertyTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public Task<List<Group>> GetAllAsync(Guid? userId)
        {
            return userId.HasValue ? this.GetAllAsync<Group>(g => g.UserId.Equals(userId)) : this.GetAllAsync();
        }
    }
}
