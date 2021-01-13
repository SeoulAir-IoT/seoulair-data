using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SeoulAir.Data.Repositories.Entities
{
    public class BaseEntityWithId
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
