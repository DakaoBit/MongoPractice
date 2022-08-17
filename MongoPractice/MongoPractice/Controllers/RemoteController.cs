using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoPractice.Services;
using System.Web;

namespace MongoPractice.Controllers
{
    /// <summary>
    /// 遠端altas Sample 練習
    /// </summary>
    public class RemoteController : Controller
    {
        private MongoDBService _mongoDBService;

        public RemoteController(IServiceProvider service)
        {
            _mongoDBService = service.GetService<MongoDBService>();
        }

        /// <summary>
        /// Use MQL find title:Blacksmith Scene 
        /// </summary>
        /// <returns></returns>
        public IActionResult Query1()
        {

            var db = _mongoDBService.Connection(Util._remoteConStr, "sample_mflix");
            var collection = db.GetCollection<BsonDocument>("movies");

            var result = collection.Find("{title: 'Blacksmith Scene'}").FirstOrDefault();
            return Json(result.ToJson());
        }

        /// <summary>
        /// query on the collection object to find the top 10 longest movies 
        /// </summary>
        /// <returns></returns>
        public IActionResult Query2()
        {
            string text = "";
            var db = _mongoDBService.Connection(Util._remoteConStr, "sample_mflix");
            var collection = db.GetCollection<BsonDocument>("movies");
            ///(sorted on the "runtime" field, descending) 
            var result = collection.Find(new BsonDocument())
                           .SortByDescending(m => m["runtime"])
                           .Limit(10)
                           .ToList();

            foreach (var r in result)
            {
                Console.WriteLine(r.GetValue("title"));
               
                text += $"{r.GetValue("title")}, " ;
            }

            return Ok(HttpUtility.HtmlDecode(text));
        }



    }
}
