using PropertyTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository.Interfaces
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        Task<List<Group>> GetAllAsync(Guid? userId);
    }
}