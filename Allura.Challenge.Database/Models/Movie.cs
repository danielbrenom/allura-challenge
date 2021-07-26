using Allura.Challenge.Database.Repositories.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace Allura.Challenge.Database.Models
{
    public class Movie : IBaseEntity
    {
        [BsonId]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Category Category { get; set; } 
    }
}