using Alura.Challenge.Database.Repositories.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace Alura.Challenge.Database.Models
{
    public class User : IBaseEntity
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}