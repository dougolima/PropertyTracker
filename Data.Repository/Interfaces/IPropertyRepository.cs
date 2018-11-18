using PropertyTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository.Interfaces
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<List<Property>> GetAllAsync(Guid searchId);
    }
}