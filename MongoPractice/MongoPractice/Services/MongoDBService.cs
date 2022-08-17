using MongoDB.Driver;

namespace MongoPractice.Services
{
    public class MongoDBService
    {
 
        public IMongoDatabase Connection(string conStr, string dbname) 
        {
            var client = new MongoClient(conStr);
            var db = client.GetDatabase(dbname);
            return db;
        }
    }
}
