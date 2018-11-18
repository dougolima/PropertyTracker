using MongoDB.Driver;
using PropertyTracker.Data.Repository.Interfaces;
using PropertyTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository
{
    public class SearchRepository : GenericRepository<Search>, ISearchRepository
    {
        public SearchRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public async Task CreateAsync(Guid groupId, Search search)
        {
            var group = await this.GetByIdAsync<Group>(groupId);

            group.Searchs = group.Searchs ?? new List<Search>();

            group.Searchs.Add(search);

            await this.UpdateOneAsync(group);
        }

        public async Task DeleteAsync(Guid groupId, Guid searchId)
        {
            var group = await this.GetByIdAsync<Group>(groupId);

            group.Searchs?.RemoveAll(s => s.Id.Equals(searchId));

            await this.UpdateOneAsync(group);
        }

        public async Task<Search> GetAsync(Guid groupId, Guid searchId)
        {
            var group = await this.GetByIdAsync<Group>(groupId);

            return group?.Searchs?.Find(s => s.Id.Equals(searchId));
        }

        public async Task<List<Search>> GetAllAsync(Guid groupId)
        {
            var group = await this.GetByIdAsync<Group>(groupId);

            return group?.Searchs ?? new List<Search>();
        }
    }
}
