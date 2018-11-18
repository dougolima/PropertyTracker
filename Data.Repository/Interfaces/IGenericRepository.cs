using MongoDB.Driver;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : IDocument
    {
        Task<List<T>> GetAllAsync();

        Task<bool> AnyAsync(Guid id);

        Task<T> GetAsync(Guid id);

        Task CreateAsync(T item);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(T item);

        Task<ReplaceOneResult> UpsertAsync(T item);
    }
}