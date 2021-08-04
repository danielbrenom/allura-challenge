using Microsoft.IdentityModel.Tokens;

namespace Alura.Challenge.Domain.Models.Data
{
    public class GrantToken
    {
        public SecurityToken Token { get; set; }
        public string UserId { get; set; }
        public string Issuer { get; set; }
        public System.DateTime GrantedOn => Token.ValidFrom;
        public System.DateTime ExpiresOn => Token.ValidTo;
        public bool IsValid => System.DateTime.Now < ExpiresOn;

    }
}