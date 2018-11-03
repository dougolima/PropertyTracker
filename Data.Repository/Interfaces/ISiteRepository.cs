﻿using PropertyTracker.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository.Interfaces
{
    public interface ISiteRepository : IGenericRepository<Site>
    {
        Task<List<Site>> GetAll(bool? active);
    }
}