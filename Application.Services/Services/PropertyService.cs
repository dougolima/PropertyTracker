using PropertyTracker.Application.DTO;
using PropertyTracker.Application.Services.Converters;
using PropertyTracker.Application.Services.Interfaces;
using PropertyTracker.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Application.Services.Services
{
    public class PropertyService : IPropertyService
    {
        private IPropertyRepository repository;

        public PropertyService(IPropertyRepository repository)
        {
            this.repository = repository;
        }

        public Task UpsertAsync(Property property)
        {
            return this.repository.UpsertAsync(property.ConvertToModel());
        }

        public async Task<List<Property>> GetAllAsync(Guid searchId)
        {
            var groups = await this.repository.GetAllAsync(searchId);

            return groups.ConvertAll(g => g.ConvertToDTO());
        }
    }
}
