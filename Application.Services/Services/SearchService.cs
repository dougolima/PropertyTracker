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

        public async Task<bool> IsValid(Guid groupId, Guid siteId)
        {
            var groupExists = await this.groupRepository.Any(groupId);
            var siteExists = await this.siteRepository.Any(groupId);

            return (groupExists && siteExists);
        }

        public Task Create(Guid groupId, Search search)
        {
            return this.searchRepository.Create(groupId, search.ConvertToModel());
        }

        public Task Delete(Guid groupId, Guid searchId)
        {
            return this.searchRepository.Delete(groupId, searchId);
        }

        public async Task<Search> Get(Guid groupId, Guid searchId)
        {
            var search = await this.searchRepository.Get(groupId, searchId);

            return search?.ConvertToDTO();
        }

        public async Task<List<Search>> GetAll(Guid groupId)
        {
            var searchs = await this.searchRepository.GetAll(groupId);

            return searchs?.ConvertAll(g => g.ConvertToDTO());
        }
    }
}
