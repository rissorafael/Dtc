using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Dtc.Domain.Entities
{
    public class ProductMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}
