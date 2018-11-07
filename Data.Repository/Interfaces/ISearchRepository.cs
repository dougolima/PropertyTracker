using PropertyTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository.Interfaces
{
    public interface ISearchRepository : IGenericRepository<Search>
    {
        Task<List<Search>> GetAll(Guid groupId);

        Task<Search> Get(Guid groupId, Guid searchId);

        Task Create(Guid groupId, Search site);

        Task Delete(Guid groupId, Guid searchId);
    }
}