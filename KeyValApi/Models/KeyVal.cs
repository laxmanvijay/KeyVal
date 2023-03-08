using MongoDB.Bson.Serialization.Attributes;

namespace KeyValApi.Models;

public class KeyValModel
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;
}