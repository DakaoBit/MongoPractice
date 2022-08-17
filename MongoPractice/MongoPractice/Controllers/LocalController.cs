using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoPractice.Models;
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

        /// <summary>
        /// 查
        /// </summary>
        /// <returns></returns>
        public IActionResult Query()
        {
            var db = _mongoDBService.Connection(Util._localConStr, "Practice");
            var collection = db.GetCollection<BsonDocument>("Guitar");
            return View();
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <returns></returns>
        public IActionResult AddGuitar()
        {
            var db = _mongoDBService.Connection(Util._localConStr, "Practice");
            var collection = db.GetCollection<Guitar>("Guitar");
            var _listGuitar = new List<Guitar>();
            var Guitar1 = new Guitar()
            {
                Id = 0,
                Make = "Gibson",
                Model = "Les Paul",
                Price = 3975.00
            };

            var Guitar2 = new Guitar()
            {
                Id = 1,
                Make = "Fender",
                Model = "Stratocaster",
                Price = 250.00
            };
            var Guitar3 = new Guitar()
            {
                Id = 2,
                Make = "Gretsch",
                Model = "Honey Dipper",
                Price = 550.00
            };
            var Guitar4 = new Guitar()
            {
                Id = 3,
                Make = "Yamaha",
                Model = "FG-12",
                Price = 300.00
            };

            //Create(InsertOne)
            //collection.InsertOne(Guitar1);

            _listGuitar.Add(Guitar1);
            _listGuitar.Add(Guitar2);
            _listGuitar.Add(Guitar3);
            _listGuitar.Add(Guitar4);

            //Create(InsertMany)
            collection.InsertMany(_listGuitar);
            return Ok("Add");
        }

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UpdateGuitar(int i)
        {
            var db = _mongoDBService.Connection(Util._localConStr, "Practice");

            switch (i)
            {
                case 1:
                    ///Update (Replace One) - BsonDocument
                    var collection = db.GetCollection<BsonDocument>("Guitar");
                    var filter = new BsonDocument("Id", 1123);
                    var replacement = new BsonDocument { { "Id", 12 }, { "price", 550 } };
                    var result = collection.ReplaceOne(filter, replacement);
                    break;
                case 2:
                    ///Update (Update One) - BsonDocument:如果DB內找不到相同ID的資料，
                    ///就會以下列的Id和price另建不符合規則的資料
                    var collection2 = db.GetCollection<BsonDocument>("Guitar");
                    var filter2 = new BsonDocument("Id", 11);
                    var update = new BsonDocument("$set", new BsonDocument("price", 550));
                    var options = new UpdateOptions { IsUpsert = true };
                    var result2 = collection2.UpdateOne(filter2, update, options);
                    break;
                case 3:
                    ///Update (with Mapping Class, Builder and LINQ) > 常用方式
                    var collection3 = db.GetCollection<Guitar>("Guitar");
                    var f = Builders<Guitar>.Filter.Eq(g => g.Id, 1);
                    var b = Builders<Guitar>.Update.Set(g => g.Price, 550);
                    var result3 = collection3.UpdateOne(f, b);
                    break;
            }

            return Ok("Update");
        }

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DeleteGuitar(int id)
        {
            var db = _mongoDBService.Connection(Util._localConStr, "Practice");
            var collection = db.GetCollection<Guitar>("Guitar");
            collection.DeleteOne(g => g.Id == id);
            return Ok("Delete");
        }
    }
}
