using PropertyTracker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Application.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<List<Property>> GetAllAsync(Guid searchId);

        Task UpsertAsync(Property property);
    }
}
