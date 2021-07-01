using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbTest
{
   public class UserModel
    {
        [BsonId]
        public ObjectId id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public AddressModel PrimaryAddress { get; set; }

        [BsonElement("dob")]
        public DateTime DateOfBirth { get; set; }
    }
}
