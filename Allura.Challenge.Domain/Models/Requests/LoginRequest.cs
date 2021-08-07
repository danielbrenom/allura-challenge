using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Requests
{
    public class LoginRequest
    {
        /// <summary>
        /// Email do usuario
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha do usuario
        /// </summary>
        [JsonPropertyName("senha")]
        [JsonProperty("senha")]
        public string Password { get; set; }
    }
}