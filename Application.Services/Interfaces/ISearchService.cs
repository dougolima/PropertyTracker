using PropertyTracker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Application.Services.Interfaces
{
    public interface ISearchService
    {
        Task<List<Search>> GetAll(Guid groupId);

        Task<bool> IsValidAsync(Guid groupId, Guid siteId);

        Task<Search> GetAsync(Guid groupId, Guid searchId);

        Task CreateAsync(Guid groupId, Search site);

        Task DeleteAsync(Guid groupId, Guid searchId);
    }
}
