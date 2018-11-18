using PropertyTracker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Application.Services.Interfaces
{
    public interface IGroupService
    {
        Task<List<Group>> GetAllAsync(Guid? userId = null);

        Task<Group> GetAsync(Guid id);

        Task CreateAsync(Group group);

        Task UpdateAsync(Group group);

        Task DeleteAsync(Guid id);
    }
}
