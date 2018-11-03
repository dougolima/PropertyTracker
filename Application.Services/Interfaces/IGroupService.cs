using PropertyTracker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Application.Services.Interfaces
{
    public interface IGroupService
    {
        Task<List<Group>> GetAll(Guid userId);

        Task<Group> Get(Guid id);

        Task Create(Group group);

        Task Update(Group group);

        Task Delete(Group group);
    }
}
