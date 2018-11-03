using PropertyTracker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Application.Services.Interfaces
{
    public interface ISiteService
    {
        Task<List<Site>> GetAll();

        Task<Site> Get(Guid id);
    }
}
