using Alura.Challenge.Database.Repositories.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace Alura.Challenge.Database.Models
{
    public class Category : IBaseEntity
    {
        [BsonId]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        
        public static Category CreateDefault()
        {
            return new Category
            {
                Id = "1",
                Title = "LIVRE",
                Color = "verde"
            };
        }
    }
}