using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver; // Using MongoDB driver 2.5.1

using ApiMongoDb.Models;

namespace ApiMongoDb.Services
{
    class TransactionService
    {
        private string connectionString = "mongodb://localhost:27017";
        private string dbName = "transactionDataBase";
        private string collectionName = "transaction";

        private MongoClient client;
        private IMongoDatabase db;

        public TransactionService()
        {
            // connect to mongo server
            this.client = new MongoClient(connectionString);
            // get Database
            this.db = client.GetDatabase(dbName);

        }

        public async Task<Transaction> GetProduct(ObjectId id)
        {
            try
            {
                return await db.GetCollection<Transaction>(collectionName).Find(p => p.Id == id).SingleAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public void Create(Transaction transaction)
        {
            try
            {
                db.GetCollection<Transaction>(collectionName).InsertOne(transaction);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void Update(ObjectId id, Transaction transaction)
        {

            var filter = Builders<Transaction>.Filter.Eq(s => s.Id, id);
            var update = Builders<Transaction>.Update.Set(p => p.Id, id);

            //
            if (!(transaction.CreatedDate==null))
            {
                update = update.Set(p => p.CreatedDate, transaction.CreatedDate);
            }
            if (!(transaction.EndDate==null))
            {
                update = update.Set(p => p.EndDate, transaction.EndDate);
            }
            //
            if (!(transaction.Step == 0))
            {
                update = update.Set(p => p.Step, transaction.Step);
            }
            if (!string.IsNullOrWhiteSpace(transaction.UserName))
            {
                update = update.Set(p => p.UserName, transaction.UserName);
            }

            //
            if (!(transaction.UserId==null))
            {
                update = update.Set(p => p.UserId, transaction.UserId);
            }
            
            //
            if (!string.IsNullOrWhiteSpace(transaction.IpAddress))
            {
                update = update.Set(p => p.IpAddress, transaction.IpAddress);
            }

            try
            {
                db.GetCollection<Transaction>(collectionName).UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void Delete(ObjectId id)
        {
            var filter = Builders<Transaction>.Filter.Eq(s => s.Id, id);
            db.GetCollection<Transaction>(collectionName).DeleteOne(filter);
        }
    }

}
