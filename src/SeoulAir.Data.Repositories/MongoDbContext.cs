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

        public MongoDbContext(AppSettings settings)
        {
            var mongoSettings = settings.mongoDbSettings;
            var credential = MongoCredential.CreateCredential("admin", mongoSettings.Username, mongoSettings.Password);
            var mongoClientSetting = MongoClientSettings.FromConnectionString(mongoSettings.ConnectionString);
            mongoClientSetting.Credential = credential;

            _database = new MongoClient(mongoClientSetting).GetDatabase(mongoSettings.DatabaseName);
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
