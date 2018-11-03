using PropertyTracker.Application.DTO;
using PropertyTracker.Application.Services.Converters;
using PropertyTracker.Application.Services.Interfaces;
using PropertyTracker.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Application.Services.Services
{
    public class GroupService : IGroupService
    {
        private IGroupRepository repository;

        public GroupService(IGroupRepository repository)
        {
            this.repository = repository;
        }

        public Task Create(Group group)
        {
            return this.repository.Create(group.ConvertToModel());
        }

        public Task Delete(Guid id)
        {
            return this.repository.Delete(id);
        }

        public async Task<Group> Get(Guid id)
        {
            var group = await this.repository.Get(id);

            return group?.ConvertToDTO();
        }

        public async Task<List<Group>> GetAll(Guid userId)
        {
            var groups = await this.repository.GetAll(userId);

            return groups.ConvertAll(g => g.ConvertToDTO());
        }

        public Task Update(Group group)
        {
            return this.repository.Update(group.ConvertToModel());
        }
    }
}
