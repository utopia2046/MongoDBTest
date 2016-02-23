using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MongoDBTest
{
    [BsonDiscriminator(RootClass = true)]
    [BsonIgnoreExtraElements]
    public class Grade
    {
        public ObjectId Id { get; set; }

        [BsonElement("student_id")]
        public Int32 StudentId { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("score")]
        public double Score { get; set; }
    }
}
