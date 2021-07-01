using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbTest
{
    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadById<T>(string table, ObjectId id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", id);

            return collection.Find(filter).FirstOrDefault();
        }

        // if record exists update, orherwise create
        public void UpsertRecord<T>(string table, ObjectId id, T record)
        {
            var collection = db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                new BsonDocument("_id", id), // if you find one matching id, delete that record
                record,// and put this record in its place otherwise insert this record into the collection if IsUpsert is true
                new UpdateOptions { IsUpsert = true });
        }

        public void DeleteRecord<T>(string table, ObjectId id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", id);

            collection.DeleteOne(filter);
        }
    }
}
