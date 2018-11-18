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

        public Task CreateAsync(Group group)
        {
            return this.repository.CreateAsync(group.ConvertToModel());
        }

        public Task DeleteAsync(Guid id)
        {
            return this.repository.DeleteAsync(id);
        }

        public async Task<Group> GetAsync(Guid id)
        {
            var group = await this.repository.GetAsync(id);

            return group?.ConvertToDTO();
        }

        public async Task<List<Group>> GetAllAsync(Guid? userId = null)
        {
            var groups = await this.repository.GetAllAsync(userId);

            return groups.ConvertAll(g => g.ConvertToDTO());
        }

        public Task UpdateAsync(Group group)
        {
            return this.repository.UpdateAsync(group.ConvertToModel());
        }
    }
}
