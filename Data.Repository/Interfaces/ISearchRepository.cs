using PropertyTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository.Interfaces
{
    public interface ISearchRepository : IGenericRepository<Search>
    {
        Task<List<Search>> GetAllAsync(Guid groupId);

        Task<Search> GetAsync(Guid groupId, Guid searchId);

        Task CreateAsync(Guid groupId, Search site);

        Task DeleteAsync(Guid groupId, Guid searchId);
    }
}