using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MongoPractice.Models.Remote
{
    public class Rating
    {
        [BsonElement("rating")]
        public double RatingScore { get; set; }

        public int NumReviews { get; set; }

        public int Meter { get; set; }
    }
}
