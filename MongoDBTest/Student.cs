using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace MongoDBTest
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator(RootClass = true)]
    public class Student
    {
        [BsonId]
        [BsonElement("_id")]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("scores")]
        public List<ScoreRecord> Scores { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class ScoreRecord
    {
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("score")]
        public double Score { get; set; }

        public override string ToString()
        {
            return String.Format("{{type: '{0}', score: {1}}}", Type, Score);
        }
    }

}
