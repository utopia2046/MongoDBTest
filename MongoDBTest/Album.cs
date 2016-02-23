using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace MongoDBTest
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator(RootClass = true)]
    public class Album
    {
        [BsonId]
        [BsonElement("_id")]
        public int Id { get; set; }

        [BsonElement("images")]
        public List<int> ImageIds { get; set; }
    }

    [BsonIgnoreExtraElements]
    [BsonDiscriminator(RootClass = true)]
    public class Image
    {
        [BsonId]
        [BsonElement("_id")]
        public int Id { get; set; }

        [BsonElement("height")]
        public int Height { get; set; }

        [BsonElement("width")]
        public int Width { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; }
    }
}