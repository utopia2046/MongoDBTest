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
    public class DBAccess
    {
        private static MongoClient _client;
        private static IMongoDatabase _database;
        private static IMongoCollection<Student> _collection;
        private static FilterDefinitionBuilder<Student> _builder;

        private DBAccess() {}

        public static IMongoCollection<Student> CreateConnection(string connString, string db, string collectionName)
        {
            try
            {
                _client = new MongoClient(connString);
                Console.WriteLine("Client created on connection string \'{0}\'", connString);

                _database = _client.GetDatabase(db);
                Console.WriteLine("Database {0} connected.", db);

                _collection = _database.GetCollection<Student>(collectionName);
                Console.WriteLine("Data collection {0} selected", collectionName);

                _builder = Builders<Student>.Filter;

                return _collection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurs: " + ex.ToString());
            }

            return null;
        }

        private static async Task<List<Student>> GetStudentsAsync()
        {
            var filter = _builder.Gte("_id", 0);
            var sort = Builders<Student>.Sort.Ascending("_id");

            return await _collection.Find(filter).Sort(sort).ToListAsync();
        }

        public static List<Student> GetStudents()
        {
            return GetStudentsAsync().GetAwaiter().GetResult();
        }

        private static async Task<ReplaceOneResult> UpdateStudent(int id, Student newRecord)
        {
            var filter = _builder.Eq("_id", id);
            return await _collection.ReplaceOneAsync(filter: filter, replacement: newRecord);
        }

        public static void UpdateStudent(Student old, Student newRecord)
        {
            var result = UpdateStudent(old.Id, newRecord).GetAwaiter().GetResult();
            Console.WriteLine("Updated {0} record", result.ModifiedCount);
        }
    }
}
