using MongoDB.Bson.Serialization.Attributes;

namespace MongoPractice.Models
{
    public class Guitar
    {
        [BsonId]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }


        /// Another Expression
        //[BsonId]
        //public int GuitarId { get; set; }
        //[BsonElement("make")]
        //public string Manufacturer { get; set; }
        //[BsonElement("model")]
        //public string ModelName { get; set; }
        //[BsonElement("price")]
        //public double PriceInDollars { get; set; }
    }
}
