using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Alura.Challenge.Database.Repositories.Interfaces;
using MongoDB.Driver;

namespace Alura.Challenge.Database.Repositories.Base
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
            settings.WriteEncoding = new UTF8Encoding();
            settings.ReadEncoding = new UTF8Encoding();
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

        public IQueryable<T> GetAll(Expression<Func<T, bool>> query)
        {
            var result = Collection.Find(query);
            return result.ToEnumerable().AsQueryable();
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            var result = await Collection.FindAsync(e => true);
            return result.ToEnumerable().AsQueryable();
        }

        public async Task<IQueryable<T>> GetAllAsync(IBasePaginator paginator)
        {
            return await Task.Run(() =>
            {
                var result = Collection.Find(_ => true).Skip(paginator.FirstItemOnPage())
                                       .Limit(paginator.ItemsPerPage);
                return result.ToEnumerable().AsQueryable();
            });
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> query)
        {
            var result = await Collection.FindAsync(query);
            return result.ToEnumerable().AsQueryable();
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> query, IBasePaginator paginator)
        {
            return await Task.Run(() =>
            {
                var result = Collection.Find(query).Skip(paginator.FirstItemOnPage())
                                       .Limit(paginator.ItemsPerPage);
                return result.ToEnumerable().AsQueryable();
            });
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
            var result = Collection.ReplaceOne(e => e.Id == entity.Id, entity);
            return result != null;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var result = await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
            return result != null;
        }

        public bool Delete(T entity)
        {
            var result = Collection.FindOneAndDelete(e => e.Id == entity.Id);
            return result != null;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var result = await Collection.FindOneAndDeleteAsync(e => e.Id == entity.Id);
            return result != null;
        }
    }
}