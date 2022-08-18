using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoPractice.Models.Remote;
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
            return Ok(result.ToJson());
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

        /// <summary>
        /// 非同步
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> BasicRead(CancellationToken cancellation = default)
        {
            ///CamelCase
            var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

            var db = _mongoDBService.Connection(Util._remoteConStr, "sample_mflix");

            ///MQL
            //var collection0 = db.GetCollection<BsonDocument>("movies");
            //var result = await collection0.Find("{title: 'Blacksmith Scene'}").FirstOrDefaultAsync();

            ///Builder
            var collection1 = db.GetCollection<Movie>("movies");
            var filter = new BsonDocument("title", "The Princess Bride");
            var betterFilter = Builders<Movie>.Filter.Eq(m => m.Title, "The Princess Bride");

            var movies = await collection1.Find<Movie>(betterFilter).ToListAsync();
            Console.WriteLine(movies.Count.ToString());
            Console.WriteLine("title: "+ movies.First().Title);
    
 
            var movie = movies.First();
            //projection 等同 select
            /* Now, if we look at the information returned, the Movie document
             * is quite large. What if we want to only return a small subset of
             * the data in a Movie? We use MongoDB projection for that, and
             * a projection filter is created the same way as a query filter.
             * Let's ask the driver to return only the Title, Year, and Cast for
             * the movie. Note that the Id field is always included in a projection
             * unless we explicitly exclude it. We do not need to explicitly
             * exclude any other fields.
             */

            var projectionFilter = Builders<Movie>.Projection
                .Include(m => m.Title)
                .Include(m => m.Year)
                .Include(m => m.Cast)
                .Exclude(m => m.Id);

            /* This projection is the same as thi MQL expression:
             *
             * "{ title: 1, year: 1, cast: 1, _id: 0 }"
             */

            /* We'll use the same query filter, and since we already know that
             * the query filter returns a single document, we can call
             * FirstOrDefaultAsync() instead of casting to a List. So let's
             * add our Projection filter:
             */
            var movieProjected = await collection1
                .Find<Movie>(betterFilter)
                .Project<Movie>(projectionFilter)
                .FirstOrDefaultAsync();

            Console.WriteLine(movieProjected.Title);
            Console.WriteLine(movieProjected.Year);
            Console.WriteLine(movieProjected.Cast.Count);

            // Every other property should be null!
            Console.WriteLine(movieProjected.Id);
            Console.WriteLine(movieProjected.Awards);


            return Ok(movies.ToJson());
        }

    }
}
