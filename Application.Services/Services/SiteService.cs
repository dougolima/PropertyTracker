using PropertyTracker.Application.DTO;
using PropertyTracker.Application.Services.Converters;
using PropertyTracker.Application.Services.Interfaces;
using PropertyTracker.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Application.Services.Services
{
    public class SiteService : ISiteService
    {
        private ISiteRepository repository;

        public SiteService(ISiteRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Site>> GetAllAsync()
        {
            var sites = await this.repository.GetAllAsync(true);

            return sites.ConvertAll(s => s.ConvertToDTO());
        }

        public async Task<Site> GetAsync(Guid id)
        {
            var site = await this.repository.GetAsync(id);

            return site?.ConvertToDTO();
        }

        public Task CreateAsync(Site site)
        {
            return this.repository.CreateAsync(site.ConvertToModel());
        }
    }
}
