using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoPractice.Services;

namespace MongoPractice.Controllers
{
    /// <summary>
    /// Local DB 練習 - Practice Guitar
    /// </summary>
    public class LocalController : Controller
    {
        private MongoDBService _mongoDBService;

        public LocalController(IServiceProvider service)
        {
            _mongoDBService = service.GetService<MongoDBService>();
        }

        public IActionResult Query()
        {
            var db = _mongoDBService.Connection(Util._localConStr, "Practice");
            var collection = db.GetCollection<BsonDocument>("Guitar");
            return View();
        }

        #region 新增
        //var guitarCollection = db.GetCollection<Guitar>("Guitar");
        //var _listGuitar = new List<Guitar>();
        //var Guitar1 = new Guitar()
        //{
        //    Id = 0,
        //    Make = "Gibson",
        //    Model = "Les Paul",
        //    Price = 3975.00
        //};

        //var Guitar2 = new Guitar()
        //{
        //    Id = 1,
        //    Make = "Fender",
        //    Model = "Stratocaster",
        //    Price = 250.00
        //};
        //var Guitar3 = new Guitar()
        //{
        //    Id = 2,
        //    Make = "Gretsch",
        //    Model = "Honey Dipper",
        //    Price = 550.00
        //};
        //var Guitar4 = new Guitar()
        //{
        //    Id = 3,
        //    Make = "Yamaha",
        //    Model = "FG-12",
        //    Price = 300.00
        //};

        ////Create(InsertOne)
        ////guitarCollection.InsertOne(Guitar1);

        //_listGuitar.Add(Guitar1);
        //_listGuitar.Add(Guitar2);
        //_listGuitar.Add(Guitar3);
        //_listGuitar.Add(Guitar4);

        ////Create(InsertMany)
        ////guitarCollection.InsertMany(_listGuitar);
        #endregion

        #region 改
        ///Update (Replace One) - BsonDocument
        //var guitarCollection = db.GetCollection<BsonDocument>("Guitar");
        //var filter = new BsonDocument("Id", 12340);
        //var replacement = new BsonDocument{{"Id", 1234},{ "price", 700 }};
        //var result = guitarCollection.ReplaceOne(filter, replacement);

        ///Update (Update One) - BsonDocument
        //var guitarCollection = db.GetCollection<BsonDocument>("Guitar");
        //var filter = new BsonDocument("Id", 1234);
        //var update = new BsonDocument("$set", new BsonDocument("price", 700));
        //var options = new UpdateOptions { IsUpsert = true };
        //var result = guitarCollection.UpdateOne(filter, update, options);

        ///Update (with Mapping Class, Builder and LINQ)
        //var guitarCollection = db.GetCollection<Guitar>("Guitar");
        //var f = Builders<Guitar>.Filter.Eq(g => g.Id, 1234);
        //var b = Builders<Guitar>.Update.Set(g => g.Price, 700);
        //guitarCollection.UpdateOne(f, b);
        #endregion

        #region 刪
        //var guitarCollection = db.GetCollection<Guitar>("Guitar");
        //guitarCollection.DeleteOne(g => g.Id == 1234);
        #endregion

        #region 查

        #endregion

    }
}
