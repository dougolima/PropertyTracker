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

        public Task<List<T>> GetAll()
        {
            return this.GetCollection<T>().AsQueryable().ToListAsync();
        }

        public Task<bool> Any(Guid id)
        {
            return this.AnyAsync<T>(x => x.Id.Equals(id));
        }

        public Task<T> Get(Guid id)
        {
            return this.GetByIdAsync<T>(id);
        }

        public Task Create(T item)
        {
            return this.AddOneAsync<T>(item);
        }

        public Task Delete(Guid id)
        {
            return this.DeleteOneAsync<T>(x => x.Id.Equals(id));
        }

        public Task Update(T item)
        {
            return this.UpdateOneAsync<T>(item);
        }
    }
}
