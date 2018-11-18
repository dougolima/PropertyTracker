using PropertyTracker.Application.DTO;
using PropertyTracker.Application.Services.Converters;
using PropertyTracker.Application.Services.Interfaces;
using PropertyTracker.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Application.Services.Services
{
    public class SearchService : ISearchService
    {
        private ISearchRepository searchRepository;
        private IGroupRepository groupRepository;
        private ISiteRepository siteRepository;

        public SearchService(
            ISearchRepository searchRepository,
            IGroupRepository groupRepository,
            ISiteRepository siteRepository)
        {
            this.searchRepository = searchRepository;
            this.groupRepository = groupRepository;
            this.siteRepository = siteRepository;
        }

        public async Task<bool> IsValidAsync(Guid groupId, Guid siteId)
        {
            var groupExists = await this.groupRepository.AnyAsync(groupId);
            var siteExists = await this.siteRepository.AnyAsync(groupId);

            return (groupExists && siteExists);
        }

        public Task CreateAsync(Guid groupId, Search search)
        {
            return this.searchRepository.CreateAsync(groupId, search.ConvertToModel());
        }

        public Task DeleteAsync(Guid groupId, Guid searchId)
        {
            return this.searchRepository.DeleteAsync(groupId, searchId);
        }

        public async Task<Search> GetAsync(Guid groupId, Guid searchId)
        {
            var search = await this.searchRepository.GetAsync(groupId, searchId);

            return search?.ConvertToDTO();
        }

        public async Task<List<Search>> GetAll(Guid groupId)
        {
            var searchs = await this.searchRepository.GetAllAsync(groupId);

            return searchs?.ConvertAll(g => g.ConvertToDTO());
        }
    }
}
