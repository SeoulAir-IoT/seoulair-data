using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SeoulAir.Data.Repositories.Entities
{
    public class BaseEntityWithId
    {
        [BsonId]
        public ObjectId Id { get; set; }

    }
}
