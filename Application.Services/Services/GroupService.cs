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

        public Task Delete(Group group)
        {
            throw new NotImplementedException();
            //return this.repository.Get(group.ConvertToModel());
        }

        public Task<Group> Get(Guid id)
        {
            return this.Get(id);
        }

        public async Task<List<Group>> GetAll(Guid userId)
        {
            var groups = await this.repository.GetAll(userId);

            return groups.ConvertAll(g => g.ConvertToDTO());
        }

        public Task Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
