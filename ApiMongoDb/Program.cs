using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

using ApiMongoDb.Models;
using ApiMongoDb.Services;

namespace ApiMongoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Transaction transaction = new Transaction
            {
                Id = ObjectId.GenerateNewId(),
                CreatedDate = new DateTime(2008, 3, 9),
                EndDate = new DateTime(2008, 4, 10),
                Step = 2,
                UserName= "this is username" ,
                UserId = ObjectId.GenerateNewId(),
                IpAddress = "127.0.2.345"
            };

            Transaction transactionUpdate = new Transaction
            {
                CreatedDate = new DateTime(2008, 10, 29),
                EndDate = new DateTime(2009, 4, 20),
                Step = 10,
                UserName = "22222 this is username",
                UserId = ObjectId.GenerateNewId(),
                IpAddress = "127.222.222.15"
            };

            ;

            Console.WriteLine("--------Connect to Mongodb-------------");
            TransactionService transactionService = new TransactionService();

            Console.WriteLine("*** Test create and get transaction ***");
            transactionService.Create(transaction);
            var transactionGet = transactionService.GetProduct(transaction.Id);

            Console.WriteLine("  +) Transaction Created:  ");
            Console.WriteLine("Id: " + transactionGet.Result.Id + "\n" +
                              "CreatedDate: " + transactionGet.Result.CreatedDate + "\n" +
                              "EndDate: " + transactionGet.Result.EndDate + "\n" +
                              "Step: " + transactionGet.Result.Step + "\n" +
                              "UserName: " + transactionGet.Result.UserName + "\n" +
                              "UserId: " + transactionGet.Result.UserId + "\n" +
                              "IpAddress: " + transactionGet.Result.IpAddress + "\n" );

            Console.WriteLine("*** Test update transaction ***");
            transactionService.Update(transaction.Id, transactionUpdate);
            var transactionGet2 = transactionService.GetProduct(transaction.Id);
            Console.WriteLine("  +) Student updated:  ");
            Console.WriteLine("Id: " + transactionGet2.Result.Id + "\n" +
                              "CreatedDate: " + transactionGet2.Result.CreatedDate + "\n" +
                              "EndDate: " + transactionGet2.Result.EndDate + "\n" +
                              "Step: " + transactionGet2.Result.Step + "\n" +
                              "UserName: " + transactionGet2.Result.UserName + "\n" +
                              "UserId: " + transactionGet2.Result.UserId + "\n" +
                              "IpAddress: " + transactionGet2.Result.IpAddress + "\n");

            Console.WriteLine("*** Test delete transaction ***");
            transactionService.Delete(transaction.Id);
            if (transactionService.GetProduct(transaction.Id).Result == null)
            {
                Console.WriteLine("transaction deleted");
            }
            else
            {
                Console.WriteLine("error");
            }
            Console.ReadLine();
        }
    
    }
}
