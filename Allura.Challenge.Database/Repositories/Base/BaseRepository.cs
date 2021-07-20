using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Allura.Challenge.Database.Repositories.Interfaces;
using MongoDB.Driver;

namespace Allura.Challenge.Database.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : IBaseEntity
    {
        private readonly MongoClient Client;
        private readonly IMongoDatabase _database;
        protected virtual IMongoCollection<T> Collection => _database.GetCollection<T>(CollectionName);
        protected abstract string CollectionName { get; }

        public BaseRepository(IConnectionSettings connectionSettings)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionSettings.ConnectionString));
            settings.SslSettings = new SslSettings {EnabledSslProtocols = SslProtocols.Tls12};
            Client = new MongoClient(settings);
            _database = Client.GetDatabase(connectionSettings.DatabaseName);
        }

        public bool Exists(string id)
        {
            return Collection.Find(e => e.Id == id).Any();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var result = await Collection.FindAsync(e => e.Id == id);
            return await result.AnyAsync();
        }

        public T Get(string id)
        {
            return Collection.Find(e => e.Id == id).FirstOrDefault();
        }

        public async Task<T> GetAsync(string id)
        {
            var result = await Collection.FindAsync(e => e.Id == id);
            return result.FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            var result = Collection.Find(e => true);
            return result.ToEnumerable().AsQueryable();
        }

        public IQueryable<T> GetAll(Func<T, bool> query)
        {
            var result = Collection.Find(e => query.Invoke(e));
            return result.ToEnumerable().AsQueryable();
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            var result = await Collection.FindAsync(e => true);
            return result.ToEnumerable().AsQueryable();
        }

        public async Task<IQueryable<T>> GetAllAsync(Func<T, bool> query)
        {
            var result = await Collection.FindAsync(e => query.Invoke(e));
            return result.ToEnumerable().AsQueryable();
        }

        public bool Add(T entity)
        {
            Collection.InsertOne(entity);
            return true;
        }

        public async Task<bool> AddAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
            return true;
        }

        public bool Update(T entity)
        {
            try
            {
                var result = Collection.ReplaceOne(e => e.Id == entity.Id, entity);
                return result != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                var result = await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
                return result != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                var result = Collection.FindOneAndDelete(e => e.Id == entity.Id);
                return result != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                var result = await Collection.FindOneAndDeleteAsync(e => e.Id == entity.Id);
                return result != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}