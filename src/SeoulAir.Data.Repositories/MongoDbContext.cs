using MongoDB.Driver;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Repositories;
using SeoulAir.Data.Repositories.Attributes;
using System;

namespace SeoulAir.Data.Repositories
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(MongoDbSettings settings)
        {
            var credential = MongoCredential.CreateCredential("admin", settings.Username, settings.Password);
            var mongoSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            mongoSettings.Credential = credential;

            _database = new MongoClient(mongoSettings).GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>()
        {
            return _database.GetCollection<TDocument>(GetCollectionName<TDocument>());
        }

        private protected string GetCollectionName<TDocument>()
        {
            var collectionAttribute = (BsonCollectionAttribute)Attribute
                .GetCustomAttribute(typeof(TDocument), typeof(BsonCollectionAttribute));
            return collectionAttribute.CollectionName;
        }
    }
}
