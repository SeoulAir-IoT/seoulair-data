using MongoDB.Driver;
using SeoulAir.Data.Domain.Interfaces.Repositories;
using SeoulAir.Data.Repositories.Attributes;
using System;
using Microsoft.Extensions.Options;
using SeoulAir.Data.Domain.Options;

namespace SeoulAir.Data.Repositories
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbOptions> mongoDbConfiguration)
        {
            _database = GetMongoDatabase(mongoDbConfiguration.Value);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>()
        {
            return _database.GetCollection<TDocument>(GetCollectionName<TDocument>());
        }

        private string GetCollectionName<TDocument>()
        {
            var collectionAttribute = (BsonCollectionAttribute)Attribute
                .GetCustomAttribute(typeof(TDocument), typeof(BsonCollectionAttribute));
            return collectionAttribute.CollectionName;
        }

        private static IMongoDatabase GetMongoDatabase(MongoDbOptions mongoConfiguration)
        {
            MongoCredential credential = MongoCredential.CreateCredential("admin",
                mongoConfiguration.Username,
                mongoConfiguration.Password);

            MongoClientSettings mongoSettings =
                MongoClientSettings.FromConnectionString(mongoConfiguration.ConnectionString);
            mongoSettings.Credential = credential;
            
            return new MongoClient(mongoSettings).GetDatabase(mongoConfiguration.DatabaseName);
        }
    }
}
