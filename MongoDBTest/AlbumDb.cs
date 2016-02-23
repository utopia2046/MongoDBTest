using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MongoDBTest
{
    class AlbumDb
    {
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<Album> albums;
        IMongoCollection<Image> images;

        public AlbumDb()
        {
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("test");
            albums = database.GetCollection<Album>("albums");
            images = database.GetCollection<Image>("images");
        }
    }
}
