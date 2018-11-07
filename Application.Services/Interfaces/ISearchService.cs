using PropertyTracker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Application.Services.Interfaces
{
    public interface ISearchService
    {
        Task<List<Search>> GetAll(Guid groupId);

        Task<bool> IsValid(Guid groupId, Guid siteId);

        Task<Search> Get(Guid groupId, Guid searchId);

        Task Create(Guid groupId, Search site);

        Task Delete(Guid groupId, Guid searchId);
    }
}
