using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MongoPractice.Models.Remote
{
    public class Awards
    {
        public int Wins { get; set; }

        public int Nominations { get; set; }

        public string Text { get; set; }
    }
}
