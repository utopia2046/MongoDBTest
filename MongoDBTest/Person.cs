using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace MongoDBTest
{
    class Person
    {
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        public int Age { get; set; }

        public List<string> Colors { get; set; }
        public List<Pet> Pets { get; set; }

        //[BsonRepresentation(BsonType.Array)]
        public BsonDocument ExtraElements { get; set; }
    }

    class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
