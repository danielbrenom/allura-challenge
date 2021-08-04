using Alura.Challenge.Database.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization.Attributes;

namespace Alura.Challenge.Database.Models
{
    public class GrantToken : IBaseEntity
    {
        [BsonId]
        public string Id { get; set; }
        public SecurityToken Token { get; set; }
        public string UserId { get; set; }
        public string Issuer { get; set; }
        public System.DateTime GrantedOn => Token.ValidFrom;
        public System.DateTime ExpiresOn => Token.ValidTo;
    }
}