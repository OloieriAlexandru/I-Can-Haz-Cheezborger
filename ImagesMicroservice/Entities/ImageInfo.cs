using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class ImageInfo
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string FilePath { get; set; }

        public string ImageType { get; set; }
    }
}
