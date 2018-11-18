using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;
using PropertyTracker.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyTracker.Data.Repository
{
    public class GenericRepository<T> : BaseMongoRepository, IGenericRepository<T> where T : IDocument
    {
        public GenericRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public Task<List<T>> GetAllAsync()
        {
            return this.GetCollection<T>().AsQueryable().ToListAsync();
        }

        public Task<bool> AnyAsync(Guid id)
        {
            return this.AnyAsync<T>(x => x.Id.Equals(id));
        }

        public Task<T> GetAsync(Guid id)
        {
            return this.GetByIdAsync<T>(id);
        }

        public Task CreateAsync(T item)
        {
            return this.AddOneAsync<T>(item);
        }

        public Task DeleteAsync(Guid id)
        {
            return this.DeleteOneAsync<T>(x => x.Id.Equals(id));
        }

        public Task UpdateAsync(T item)
        {
            return this.UpdateOneAsync<T>(item);
        }

        public Task<ReplaceOneResult> UpsertAsync(T item)
        {
            return this.GetCollection<T>().ReplaceOneAsync(
                doc => doc.Id.Equals(item.Id),
                item,
                new UpdateOptions { IsUpsert = true });
        }
    }
}
