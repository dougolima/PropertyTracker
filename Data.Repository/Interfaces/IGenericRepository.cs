using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : IDocument
    {
        Task<List<T>> GetAll();

        Task<T> Get(Guid id);

        Task Create(T item);
    }
}