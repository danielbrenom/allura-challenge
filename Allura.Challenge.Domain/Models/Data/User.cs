using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Data
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}