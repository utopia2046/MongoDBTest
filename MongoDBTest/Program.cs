using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace MongoDBTest
{
    class Program
    {
        //static MongoClient client = new MongoClient("mongodb://localhost:27017");
        //static IMongoDatabase database = client.GetDatabase("students");
        //static IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("grades");


        static void Main(string[] args)
        {
            InsertTest.Test(args);

            var collection = DBAccess.CreateConnection("mongodb://localhost:27017", "school", "students");

            var studentsList = DBAccess.GetStudents();

            Console.WriteLine("Students list count = {0}. Should be 200", studentsList.Count);

            //foreach (var student in studentsList)
            //{
            //    var query = from record in student.Scores
            //                where record.Type == "homework"
            //                select record.Score;
            //    var minScore = query.Min();
            //    Console.WriteLine("Student Id = {0}, MinScore = {1}", student.Id, minScore);
            //}

            foreach (var student in studentsList)
            {
                var newRecord = CreateNewRecord(student);
                DBAccess.UpdateStudent(student, newRecord);
            }

            Console.ReadLine();
        }

        static Student CreateNewRecord(Student old)
        {
            Student newRecord = new Student
            {
                Id = old.Id,
                Name = old.Name,
                Scores = new List<ScoreRecord>()
            };

            //var query = from record in old.Scores
            //            where record.Type == "homework"
            //            orderby record.Score ascending
            //            select record;
            //
            //var minScore = query.First<ScoreRecord>();
            //Console.WriteLine("Minimal homework score for student {0} is {1}", old.Id, minScore.Score);

            foreach (var record in old.Scores)
            {
                if (record.Type == "homework")
                {
                    var homework = newRecord.Scores.FirstOrDefault<ScoreRecord>(x => x.Type == "homework");
                    if (homework != null)
                    {
                        if (homework.Score < record.Score)
                        {
                            homework.Score = record.Score;
                        }
                    }
                    else
                    {
                        newRecord.Scores.Add(new ScoreRecord { Type = "homework", Score = record.Score });
                    }
                }
                else
                {
                    newRecord.Scores.Add(new ScoreRecord { Type = record.Type, Score = record.Score });
                }
            }

            Console.WriteLine("Student _id: {0}", newRecord.Id);
            Console.WriteLine("  name: '{0}'", newRecord.Name);
            foreach (var record in newRecord.Scores)
            {
                Console.WriteLine("  " + record);
            }

            return newRecord;
        }

        /*
            //var list = GetGrades().GetAwaiter().GetResult();

            //var minRecord = GetMinimalHomeworkRecords(list);

            //DeleteRecords(minRecord);//.GetAwaiter().GetResult();
            //Console.WriteLine("Totally {0} records deleted");

        static async Task<List<BsonDocument>> GetGrades()
        {
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Lt("student_id", 36) & builder.Eq("type", "homework");
            var sort = Builders<BsonDocument>.Sort.Ascending("student_id");

            return await collection.Find(filter).Sort(sort).ToListAsync();
        }

        static Dictionary<int, Grade> GetMinimalHomeworkRecords(List<BsonDocument> list)
        {
            var minRecord = new Dictionary<int, Grade>();

            foreach (var doc in list)
            {
                Console.WriteLine("student_id: {0}, score = {1}, _id = {2}", 
                    doc["student_id"].ToString(),doc["score"].ToDouble(), doc["_id"].AsObjectId);

                var grade = new Grade
                {
                    Id = doc["_id"].AsObjectId,
                    StudentId = doc["student_id"].AsInt32,
                    Score = doc["score"].AsDouble,
                    Type = doc["type"].AsString
                };

                // generate a dictionary of each student's minimal homework score
                if (minRecord.ContainsKey(grade.StudentId))
                {
                    // replace record in minRecord dictionary if current score is lower
                    if (grade.Score < minRecord[grade.StudentId].Score)
                    {
                        minRecord[grade.StudentId] = grade;
                    }
                }
                else
                {
                    minRecord.Add(grade.StudentId, grade);
                }
            }

            Console.WriteLine("minRecord.Length = ", minRecord.Count); // should be 200
            return minRecord;
        }

        static void DeleteRecords(Dictionary<int, Grade> minRecord)
        {
            //var count = 0L;
            // delete documents in minRecord
            foreach (var record in minRecord)
            {
                Console.WriteLine("db.grades.remove({{'_id': ObjectId('{0}')}}, {{justOne:1}})", record.Value.Id.ToString());
                //var filter = Builders<BsonDocument>.Filter.Eq("_id", record.Value.Id);
                //await collection.DeleteOneAsync(filter);
                //Console.WriteLine(result.DeletedCount);
                //count += result.DeletedCount;
            }

            //return count;
        }
        */

        /*

        static BsonDocument CreateBsonDocument()
        {
            var doc = new BsonDocument
            {
                {
                    "name" , "Jones"
                }
            };

            doc.Add("age", 30);
            doc["profession"] = "hacker";
            var nestedArray = new BsonArray();
            nestedArray.Add(new BsonDocument("color", "red"));

            doc.Add("array", nestedArray);

            //Console.WriteLine(doc);
            return doc;
        }

        static Person CreatePerson()
        {
            var person = new Person
            {
                Name = "Jones",
                Age = 30,
                Colors = new List<string> { "red", "blue" },
                Pets = new List<Pet> { new Pet { Name = "Fluffy", Type = "Dog" } },
                ExtraElements = new BsonDocument("anotherName", "another property value")
            };

            using (var writer = new JsonWriter(Console.Out))
            {
                BsonSerializer.Serialize(writer, person);
            }

            BsonClassMap.RegisterClassMap(cm => {
                cm.AutoMap();
                cm.MapMember(x => x.Name).SetElementName("Name");
            });

            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("CamelCase", conventionPack);

                return person;
        }

        static async Task TestMongoAsync()
        {
            //var setttings = new MongoClientSettings();
            var connectionString = "mongodb://localhost:23838,localhost:23841,localhost:23842";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("restaurants");


            return col;

            //var collection = DBAccess.CreateConnection("mongodb://localhost:23838,localhost:23841,localhost:23842", "test", "restaurants");
        }

    */
    }
}
