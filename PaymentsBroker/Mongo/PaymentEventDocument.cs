using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentsBroker.Mongo;

public class PaymentEventDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("price")]
    public decimal Price { get; set; }
    [BsonElement("psp")]
    public string PSP { get; set; }
    [BsonElement("order_id")]
    public string OrderId { get; set; }
    [BsonElement("timestamp")]
    public DateTime Timestamp { get; set; }
}