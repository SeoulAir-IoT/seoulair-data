using MongoDB.Driver;

namespace SeoulAir.Data.Domain.Interfaces.Repositories
{
    public interface IMongoDbContext
    {
        IMongoCollection<TDocument> GetCollection<TDocument>();
    }
}
