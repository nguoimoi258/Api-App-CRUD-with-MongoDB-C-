using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ApiMongoDb.Models
{
    class Transaction
    {
        public ObjectId Id { get; set; }
        public BsonDateTime CreatedDate { get; set; }
        public BsonDateTime EndDate { get; set; }
        public int Step { get; set; }
        public string UserName { get; set; }
        public BsonObjectId UserId { get; set; }
        public string IpAddress { get; set; }
    }
}
